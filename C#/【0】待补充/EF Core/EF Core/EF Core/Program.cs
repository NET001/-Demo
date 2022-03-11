using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 简单增删改查
        /// </summary>
        static void Demo1()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                //初始化数据库
                dbContext.Database.EnsureCreated();

                Table1 table1 = dbContext.Table1.Where(t => t.Id == 1).FirstOrDefault();
                table1.Name = "newName";
                dbContext.Table1.Update(table1);
                dbContext.SaveChanges();
                dbContext.Table1.Remove(table1);
                dbContext.SaveChanges();
                dbContext.Table1.Add(table1);
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 执行原生sql
        /// </summary>
        static void Demo2()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                dbContext.Database.ExecuteSqlRaw("");
            }
        }
        //跟踪查询
        static void Demo3()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                //默认情况下跟踪查询返回的实体在修改这些查询的实体后在提交修可以同步保存到数据库中
                //EF Core 不会用数据库值覆盖该实体中实体属性的当前值和原始值。 如果未在上下文中找到该实体，EF Core 将创建新的实体实例，并将其附加到上下文。 查询结果不会包含任何已添加到上下文但尚未保存到数据库中的实体。
                var t1 = dbContext.Table1.First();
                //显示的指定为跟踪查询,可以在DbContextOptionsBuilder中配置全局跟踪行为
                var t1_1 = dbContext.Table1.AsTracking().First();
                t1.Name = "修改";
                dbContext.SaveChanges();
                //非跟踪实体查询
                var t2 = dbContext.Table1.AsNoTracking().First();
                //跟踪查询时结果多次返回相同的实体则每次会返回相同的实体，而非跟踪查询每次都会返回新的实体

                dbContext.Entry<Table1>(new Table1()).CurrentValues.SetValues(new Table1());
            }
        }
        //关联数据查询
        static void Demo4()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                dbContext.Table5
                    //单层数据预加载
                    .Include(t => t.Table6s)
                    //多层数据预加载
                    .ThenInclude(t => t.Table7s)
                    .ToList();
            }
        }
        /// <summary>
        /// 复杂查询
        /// </summary>
        static void Demo5()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                //INNER JOIN 连接查询
                var quer1 = from t1 in dbContext.Table1
                            join t2 in dbContext.Table2 on t1.Id equals t2.Id2
                            where t1.Name == "测试"
                            select new
                            {
                                t1,
                                t2
                            };
                var quer2 = from t1 in dbContext.Table1
                            from t2 in dbContext.Table2.Where(t => t1.Id == t.Id2)
                            select new
                            {
                                t1,
                                t2
                            };

                //LEFT JOIN查询
                var quer3 = from t1 in dbContext.Table1
                            from t2 in dbContext.Table2.Where(t => t1.Id == t.Id2).DefaultIfEmpty()
                            select new
                            {
                                t1,
                                t2
                            };
                var quer4 = from t1 in dbContext.Table1
                            join t2 in dbContext.Table2 on t1.Id equals t2.Id2 into grouping
                            from p in grouping.DefaultIfEmpty()
                            select new
                            {
                                t1,
                                p
                            };
                //GROUPBY
                var quer5 = from t1 in dbContext.Table1
                            group t1 by t1.Name
                            //into表示一个临时存放的标识符
                            into g
                            where g.Count() > 2
                            orderby g.Key
                            select new
                            {
                                g.Key,
                                Count = g.Count()
                            };
                //分页查询
                var quer6 = dbContext.Table1
                    .OrderBy(t => t.Name)
                    //页数
                    .Skip(1)
                    //显示值
                    .Take(10)
                    .ToList();
                //基于Keyset 分页更加高效
                var quer7 = dbContext.Table1
                    .OrderBy(b => b.Name)
                    .Where(b => b.Id > 10)
                    .Take(10)
                    .ToList();

                var quer8 = dbContext.Table1
                    .OrderBy(t => t.Name)
                    //按照多个属性排序
                    .ThenBy(t => t.Id)
                    //按照条件
                    .Where(b => b.Name == "条件")
                    //页数
                    .Skip(1)
                    //显示值
                    .Take(10)
                    .ToList();
            }
        }

        /// <summary>
        /// 原生sql(原生sql的适用会导致切换数据库的时候可能导致sql不可用,这样就需要修改代码了)
        /// </summary>
        static void Demo6()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                //类似于Dapper的查询
                var t1 = dbContext.Table1
                    .FromSqlRaw("SELECT * FROM Table1")
                    .ToList();
                //防止sql注入的方式传参
                var t2 = dbContext.Table1
                    .FromSqlInterpolated($"SELECT * FROM Table1 WHERE Id{10}")
                    .ToList();
                //混合使用(Linq将会以子查询的方式)
                var t3 = dbContext.Table1
                  .FromSqlInterpolated($"SELECT * FROM Table1 WHERE Id{10}")
                  .Where(t => t.Name == "测试")
                  .OrderByDescending(t => t.Id)
                  .ToList();

                //执行新增
                dbContext.Database.ExecuteSqlRaw("INSERT INTO Table1 VALUES('测试')");

            }
        }

        /// <summary>
        /// 进行sql函数的映射
        /// </summary>
        static void Demo7()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                var t1 = from p in dbContext.Table1
                         select dbContext.ActivePostCountForBlog(p.Id);
            }
        }
        //可以将多个增删改操作合并到一个SaveChanges中
        //级联删除
        static void Demo8()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                var q1 = dbContext
                     .Table5
                     .Include(t => t.Table6s.Where(a => t.Id == t.Id)).First();
                dbContext.Remove(q1);
                //级联删除
                dbContext.SaveChanges();
                //断开级联
                var q2 = dbContext
                     .Table5
                     .Include(t => t.Table6s.Where(a => t.Id == t.Id)).First();
                for (int i = 0; i < q2.Table6s.Count; i++)
                {
                    q2.Table6s[0] = null;
                }
                dbContext.Remove(q1);
                //级联删除
                dbContext.SaveChanges();

            }
        }
        //事务
        static void Demo9()
        {
            using (MyDbContext dbContext = new MyDbContext())
            {
                //开始事务
                using (var transtion = dbContext.Database.BeginTransaction())
                {
                    dbContext.Add(new Table1()
                    {
                        Id = 1,
                        Name = "测试"
                    });
                    dbContext.Add(new Table2()
                    {
                        Id1 = 1,
                        Id2 = 2
                    });
                    transtion.Commit();
                }
            }
        }
    }
    /// <summary>
    /// Db数据库配置上下文
    /// </summary>
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }
        #region 指定orm实体

        public DbSet<Table1> Table1 { get; set; }
        public DbSet<Table2> Table2 { get; set; }
        public DbSet<Table3> Table3 { get; set; }
        public DbSet<Table4> Table4 { get; set; }
        public DbSet<Table5> Table5 { get; set; }
        public DbSet<Table6> Table6 { get; set; }
        public DbSet<Table7> Table7 { get; set; }
        #endregion


        public int ActivePostCountForBlog(int blogId) => throw new NotSupportedException();
        /// <summary>
        /// 模型配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                return;
            //配置主键
            modelBuilder.Entity<Table1>().HasKey(t => t.Id);
            //配置多主键使用委托
            modelBuilder.Entity<Table2>(cfg =>
            {
                cfg.HasKey(t => new
                {
                    t.Id1,
                    t.Id2
                });
                //配置属性name为必填
                cfg.Property(t => t.Name).IsRequired();
                //配置名称
                cfg.Property(t => t.Name).HasColumnName("name_my");
                //配置最大长度
                cfg.Property(t => t.Name).HasMaxLength(500);
                //设置默认值
                cfg.Property(t => t.Name).HasDefaultValue("默认值");
                //执行sql的默认值
                cfg.Property(t => t.Name).HasDefaultValueSql("getdata()");
                //计算列
                cfg.Property(t => t.Name).HasComputedColumnSql("[Name]+','+[Name]");
                //值转换
                cfg.Property(t => t.Name).HasConversion<int>(t => int.Parse(t), t => t.ToString());
            });
            modelBuilder.Entity<Table3>(cfg =>
            {
                //指定主键对于IDENTITY 的EF可以自动生成
                cfg.HasKey(t => t.Id);
                //配置表名
                cfg.ToTable("Table2");
                //视图映射
                cfg.ToView("ViewTable2");
            });
            //从模型中排除
            modelBuilder.Ignore<Table4>();
            //忽略列
            modelBuilder.Entity<Table4>(cfg =>
            {
                //配置属性忽略
                cfg.Ignore(t => t.Name);
            });
            //数据种子,在执行数据迁移的时候可以生成
            modelBuilder.Entity<Table2>(cfg =>
            {
                cfg.HasData(new Table2()
                {
                    Id1 = 1,
                    Id2 = 2,
                    Name = "Name"
                });
                //指定为无主键实体
                cfg.HasNoKey();
            });
            //进行函数的映射
            modelBuilder
                .HasDbFunction(typeof(MyDbContext)
                .GetMethod(nameof(ActivePostCountForBlog), new[] { typeof(int) }))
                .HasName("CommentedPostCountForBlog");
        }
        /// <summary>
        /// 上下文配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //MySql连接字符串配置
            optionsBuilder.UseMySql("");
            //SqlServer连接字符串配置
            optionsBuilder.UseSqlServer("");
            //Sqlite连接字符串配置
            optionsBuilder.UseSqlite("");
            //配置查询默认跟踪行为
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
    public class Table1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Table2
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }
        [Column("name_my")]
        [MaxLength(500)]
        public string Name { get; set; }
    }
    //通过注释来而配置模型,但是会在OnModelCreating中被替换配置
    [Table("Table3")]
    public class Table3
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //从模型中排除
    [NotMapped]
    public class Table4
    {
        public int Id { get; set; }
        [NotMapped]
        public string Name { get; set; }
    }
    public class Table5
    {
        public int Id { get; set; }
        public List<Table6> Table6s { get; set; }
    }
    public class Table6
    {
        public int Id { get; set; }
        public List<Table7> Table7s { get; set; }
    }
    public class Table7
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
