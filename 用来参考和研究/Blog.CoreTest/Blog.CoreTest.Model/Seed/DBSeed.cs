using Blog.CoreTest.Common;
using Blog.CoreTest.Common.DB;
using Blog.CoreTest.Common.Helper;
using Blog.CoreTest.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Model.Seed
{

    //封装者库表创建流程
    public class DBSeed
    {
        private static string SeedDataFolder = "BlogCore.Data.json/{0}.tsv";


        /// <summary>
        /// 异步添加种子数据
        /// </summary>
        /// <param name="myContext"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static async Task SeedAsync(MyContext myContext, string WebRootPath)
        {
            try
            {
                if (string.IsNullOrEmpty(WebRootPath))
                {
                    throw new Exception("获取wwwroot路径时，异常！");
                }
                //组合路径

                SeedDataFolder = Path.Combine(WebRootPath, SeedDataFolder);
                //打印相应的信息
                Console.WriteLine("************ Blog.Core DataBase Set *****************");
                Console.WriteLine($"Is multi-DataBase: {Appsettings.app(new string[] { "MutiDBEnabled" })}");
                Console.WriteLine($"Is CQRS: {Appsettings.app(new string[] { "CQRSEnabled" })}");
                Console.WriteLine();
                Console.WriteLine($"Master DB ConId: {MyContext.ConnId}");
                Console.WriteLine($"Master DB Type: {MyContext.DbType}");
                Console.WriteLine($"Master DB ConnectString: {MyContext.ConnectionString}");
                Console.WriteLine();
                if (Appsettings.app(new string[] { "MutiDBEnabled" }).ObjToBool())
                {
                    var slaveIndex = 0;
                    BaseDBConfig.MutiConnectionString.allDbs.Where(x => x.ConnId != MainDb.CurrentDbConnId).ToList().ForEach(m =>
                    {
                        slaveIndex++;
                        Console.WriteLine($"Slave{slaveIndex} DB ID: {m.ConnId}");
                        Console.WriteLine($"Slave{slaveIndex} DB Type: {m.DbType}");
                        Console.WriteLine($"Slave{slaveIndex} DB ConnectString: {m.Connection}");
                        Console.WriteLine($"--------------------------------------");
                    });
                }
                else if (Appsettings.app(new string[] { "CQRSEnabled" }).ObjToBool())
                {
                    var slaveIndex = 0;
                    BaseDBConfig.MutiConnectionString.slaveDbs.Where(x => x.ConnId != MainDb.CurrentDbConnId).ToList().ForEach(m =>
                    {
                        slaveIndex++;
                        Console.WriteLine($"Slave{slaveIndex} DB ID: {m.ConnId}");
                        Console.WriteLine($"Slave{slaveIndex} DB Type: {m.DbType}");
                        Console.WriteLine($"Slave{slaveIndex} DB ConnectString: {m.Connection}");
                        Console.WriteLine($"--------------------------------------");
                    });
                }
                else
                {
                }

                Console.WriteLine();

                // 创建数据库
                Console.WriteLine($"Create Database(The Db Id:{MyContext.ConnId})...");

                if (MyContext.DbType != SqlSugar.DbType.Oracle)
                {
                    //创建库
                    var createResult=  myContext.Db.DbMaintenance.CreateDatabase();
                    ConsoleHelper.WriteSuccessLine($"Database created successfully!");
                }
                else
                {
                    //Oracle 数据库不支持该操作
                    ConsoleHelper.WriteSuccessLine($"Oracle 数据库不支持该操作，可手动创建Oracle数据库!");
                }

                // 创建数据库表，遍历指定命名空间下的class，
                // 注意不要把其他命名空间下的也添加进来。
                Console.WriteLine("Create Tables...");
                //遍历当前执行代码的程序集中的models层
                var modelTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                 where t.IsClass && t.Namespace == "Blog.Core.Model.Models"
                                 select t;
                modelTypes.ToList().ForEach(t =>
                {
                    // 这里只支持添加表，不支持删除
                    // 如果想要删除，数据库直接右键删除，或者联系SqlSugar作者；
                    //判断表不存在
                    if (!myContext.Db.DbMaintenance.IsAnyTable(t.Name))
                    {
                        Console.WriteLine(t.Name);
                        //进行codefirst
                        myContext.Db.CodeFirst.InitTables(t);
                    }
                });
                //封装的能够在控制台中打印不同颜色的字体
                ConsoleHelper.WriteSuccessLine($"Tables created successfully!");
                Console.WriteLine();


                //上面是建表，下面这个是判断是否创建数据
                if (Appsettings.app(new string[] { "AppSettings", "SeedDBDataEnabled" }).ObjToBool())
                {
                    //json序列号配置类
                    JsonSerializerSettings setting = new JsonSerializerSettings();
                    //添加json序列化全局配置
                    JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
                    {
                        //日期类型默认格式化处理
                        setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                        setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                        //空值处理
                        setting.NullValueHandling = NullValueHandling.Ignore;

                        //高级用法九中的Bool类型转换 设置
                        //setting.Converters.Add(new BoolConvert("是,否"));

                        return setting;
                    });

                    Console.WriteLine($"Seeding database data (The Db Id:{MyContext.ConnId})...");

                    #region BlogArticle
                    //判断当前表是否存在
                    //下面就给具体表赋值的内容了
                    if (!await myContext.Db.Queryable<BlogArticle>().AnyAsync())
                    {
                        //读取文件中的内容通过jsonconvert转换为实体，在通过sqlSugar进行批量插入
                        myContext.GetEntityDB<BlogArticle>().InsertRange(JsonHelper.ParseFormByJson<List<BlogArticle>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "BlogArticle"), Encoding.UTF8)));
                        Console.WriteLine("Table:BlogArticle created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:BlogArticle already exists...");
                    }
                    #endregion


                    #region Modules
                    if (!await myContext.Db.Queryable<Modules>().AnyAsync())
                    {



                        var data = JsonConvert.DeserializeObject<List<Modules>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "Modules"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<Modules>().InsertRange(data);
                        Console.WriteLine("Table:Modules created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:Modules already exists...");
                    }
                    #endregion


                    #region Permission
                    if (!await myContext.Db.Queryable<Permission>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<Permission>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "Permission"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<Permission>().InsertRange(data);
                        Console.WriteLine("Table:Permission created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:Permission already exists...");
                    }
                    #endregion


                    #region Role
                    if (!await myContext.Db.Queryable<Role>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<Role>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "Role"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<Role>().InsertRange(data);
                        Console.WriteLine("Table:Role created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:Role already exists...");
                    }
                    #endregion


                    #region RoleModulePermission
                    if (!await myContext.Db.Queryable<RoleModulePermission>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<RoleModulePermission>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "RoleModulePermission"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<RoleModulePermission>().InsertRange(data);
                        Console.WriteLine("Table:RoleModulePermission created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:RoleModulePermission already exists...");
                    }
                    #endregion


                    #region Topic
                    if (!await myContext.Db.Queryable<Topic>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<Topic>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "Topic"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<Topic>().InsertRange(data);
                        Console.WriteLine("Table:Topic created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:Topic already exists...");
                    }
                    #endregion


                    #region TopicDetail
                    if (!await myContext.Db.Queryable<TopicDetail>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<TopicDetail>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "TopicDetail"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<TopicDetail>().InsertRange(data);
                        Console.WriteLine("Table:TopicDetail created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:TopicDetail already exists...");
                    }
                    #endregion


                    #region UserRole
                    if (!await myContext.Db.Queryable<UserRole>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<UserRole>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "UserRole"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<UserRole>().InsertRange(data);
                        Console.WriteLine("Table:UserRole created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:UserRole already exists...");
                    }
                    #endregion


                    #region sysUserInfo
                    if (!await myContext.Db.Queryable<sysUserInfo>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<sysUserInfo>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "sysUserInfo"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<sysUserInfo>().InsertRange(data);
                        Console.WriteLine("Table:sysUserInfo created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:sysUserInfo already exists...");
                    }
                    #endregion


                    #region TasksQz
                    if (!await myContext.Db.Queryable<TasksQz>().AnyAsync())
                    {
                        var data = JsonConvert.DeserializeObject<List<TasksQz>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "TasksQz"), Encoding.UTF8), setting);

                        myContext.GetEntityDB<TasksQz>().InsertRange(data);
                        Console.WriteLine("Table:TasksQz created success!");
                    }
                    else
                    {
                        Console.WriteLine("Table:TasksQz already exists...");
                    }
                    #endregion

                    ConsoleHelper.WriteSuccessLine($"Done seeding database!");
                }

                Console.WriteLine();

            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"1、若是Mysql,查看常见问题:https://github.com/anjoy8/Blog.Core/issues/148#issue-776281770 \n" +
                    $"2、若是Oracle,查看常见问题:https://github.com/anjoy8/Blog.Core/issues/148#issuecomment-752340231 \n" +
                    "3、其他错误：" + ex.Message);
            }
        }
    }
}
