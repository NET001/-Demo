using Blog.Core_CJL.Common;
using Blog.Core_CJL.Common.DB;
using Blog.Core_CJL.Common.Helper;
using Blog.Core_CJL.Common.LogHelper;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions.ServiceExtensions
{
    /// <summary>
    /// SqlSugar 启动服务
    /// </summary>
    public static class SqlsugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 默认添加主数据库连接
            MainDb.CurrentDbConnId = Appsettings.app(new string[] { "MainDB" });

            // 把多个连接对象注入服务，这里必须采用Scope，因为有事务操作
            services.AddScoped<ISqlSugarClient>(o =>
            {
                // 连接字符串
                var listConfig = new List<ConnectionConfig>();
                // 从库
                var listConfig_Slave = new List<SlaveConnectionConfig>();

                //获取从库配置
                BaseDBConfig.MutiConnectionString.slaveDbs.ForEach(s =>
                {
                    //添加从库配置
                    listConfig_Slave.Add(new SlaveConnectionConfig()
                    {
                        HitRate = s.HitRate,
                        ConnectionString = s.Connection
                    });
                });

                //获取所有配置（其中包含了多库，单库，读写分离）
                BaseDBConfig.MutiConnectionString.allDbs.ForEach(m =>
                {
                    //添加所有库的配置
                    listConfig.Add(new ConnectionConfig()
                    {
                        //配置id
                        ConfigId = m.ConnId.ObjToString().ToLower(),
                        //连接字符串
                        ConnectionString = m.Connection,
                        //枚举是值类型，所以可以直接转换
                        DbType = (DbType)m.DbType,
                        //开启自动关闭
                        IsAutoCloseConnection = true,
                        // Check out more information: https://github.com/anjoy8/Blog.Core/issues/122
                        IsShardSameThread = false,
                        //[JsonIgnore]表示忽略序列号
                        //这是提供出来的切面委托方法
                        AopEvents = new AopEvents
                        {
                            //所执行sql的日志
                            OnLogExecuting = (sql, p) =>
                            {
                                if (Appsettings.app(new string[] { "AppSettings", "SqlAOP", "Enabled" }).ObjToBool())
                                {
                                    Parallel.For(0, 1, e =>
                                    {
                                        //通过miniprofiler在界面中显示执行的sql
                                        MiniProfiler.Current.CustomTiming("SQL：", GetParas(p) + "【SQL语句】：" + sql);
                                        //写入日志
                                        LogLock.OutSql2Log("SqlLog", new string[] { GetParas(p), "【SQL语句】：" + sql });

                                    });
                                }
                            }
                        },
                        //更多配置
                        MoreSettings = new ConnMoreSettings()
                        {
                            //IsWithNoLockQuery = true,
                            //自动清除缓存
                            IsAutoRemoveDataCache = true
                        },
                        // 从库
                        SlaveConnectionConfigs = listConfig_Slave,
                        // 自定义特性
                        ConfigureExternalServices = new ConfigureExternalServices()
                        {
                            EntityService = (property, column) =>
                            {
                                //当这个属性是主键并且为int类型时同时设置为自增长
                                if (column.IsPrimarykey && property.PropertyType == typeof(int))
                                {
                                    column.IsIdentity = true;
                                }
                            }
                        },
                        InitKeyType = InitKeyType.Attribute
                    }
                   );
                });

                //构造操作实体
                return new SqlSugarClient(listConfig);
            });
        }

        private static string GetParas(SugarParameter[] pars)
        {
            string key = "【SQL参数】：";
            foreach (var param in pars)
            {
                key += $"{param.ParameterName}:{param.Value}\n";
            }

            return key;
        }
    }
}
