using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Configuration_CJL
{
    class Program
    {
        static void Main(string[] args)
        {

            //Demo1();
            //Demo2();
            //Demo2();
            //Demo4();
            //Demo5(args);
            Demo8();
            Console.ReadKey();
        }
        /// <summary>
        /// 构建一个IConfiguration并访问
        /// </summary>
        static void Demo1()
        {
            Dictionary<string, string> source1 = new Dictionary<string, string>()
            {
                ["key1"] = "value1",
                ["key2"] = "value2",
                ["key3"] = "value3",
                ["key4"] = "value4",
                ["key5"] = "value5",
            };
            Dictionary<string, string> source2 = new Dictionary<string, string>()
            {
                ["key1"] = "value11",
                ["key2"] = "value22",
                ["key3"] = "value33",
                ["key4"] = "value44",
                ["key5"] = "value55",
                ["key6"] = "value66",
            };
            //添加了两个内存配置源最新的会替换掉旧的
            IConfiguration config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource() { InitialData = source1 })
                .Add(new MemoryConfigurationSource() { InitialData = source2 })
                .Build();

            Console.WriteLine(config.GetSection("key1").Value);
            Console.WriteLine(config.GetSection("key2").Value);
            Console.WriteLine(config.GetSection("key3").Value);
            Console.WriteLine(config.GetSection("key4").Value);
            Console.WriteLine(config.GetSection("key5").Value);
            Console.WriteLine(config.GetSection("key6").Value);
            //不会报错就是为空
            Console.WriteLine(config.GetSection("key7").Value);
        }
        /// <summary>
        /// 对象绑定
        /// </summary>
        static void Demo2()
        {
            Dictionary<string, string> source = new Dictionary<string, string>()
            {
                ["source:p1:p1"] = "source:p1:p1",
                ["source:p1:p2"] = "source:p1:p2",
                ["source:p2:p1"] = "source:p2:p1",
                ["source:p2:p2"] = "source:p2:p2",
            };
            //IConfigurationRoot表示为根
            //其他节点则为IConfigurationSection
            IConfigurationRoot config = new ConfigurationBuilder()
               .Add(new MemoryConfigurationSource() { InitialData = source })
               .Build();
            Obj1 obj1 = config.GetSection("source").Get<Obj1>();
            Console.WriteLine(obj1.p1.p1);
            Console.WriteLine(obj1.p1.p2);
            Console.WriteLine(obj1.p2.p1);
            Console.WriteLine(obj1.p2.p2);


        }

        /// <summary>
        /// 读取json配置文件
        /// </summary>
        static void Demo3()
        {

            IConfiguration config = new ConfigurationBuilder()
                //支持实时同步默认是关闭的
                .AddJsonFile(path: "", optional: true, reloadOnChange: true)
                .Build();
            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
            {
                Console.WriteLine("发生变更");
            });

        }
        /// <summary>
        /// 获取环境变量
        /// </summary>
        static void Demo4()
        {
            var list = Environment.GetEnvironmentVariables();

            foreach (var item in list.Keys)
            {
                Console.WriteLine(item + "=" + list[item]);
            }
        }
        /// <summary>
        /// 命令行解析
        /// </summary>
        static void Demo5(string[] args)
        {
            Dictionary<string, string> mapping = new Dictionary<string, string>()
            {
                ["-a"] = "architecture",
                ["-arch"] = "architecture",
            };
            var configuration = new ConfigurationBuilder()
                  .AddCommandLine(args, mapping)
                  .Build();
            Console.WriteLine(configuration["architecture"]);
        }

        /// <summary>
        /// 获取xml
        /// </summary>
        static void Demo6()
        {

            IConfiguration config = new ConfigurationBuilder()
                //支持实时同步默认是关闭的
                .AddXmlFile(path: "", optional: true, reloadOnChange: true)
                .Build();
            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
            {
                Console.WriteLine("发生变更");
            });

        }
        /// <summary>
        /// 获取ini
        /// </summary>
        static void Demo7()
        {

            IConfiguration config = new ConfigurationBuilder()
                //支持实时同步默认是关闭的
                .AddIniFile(path: "", optional: true, reloadOnChange: true)
                .AddIniStream(null)
                .Build();
            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
            {
                Console.WriteLine("发生变更");
            });
        }

        /// <summary>
        /// 自定义扩展数据源
        /// </summary>
        static void Demo8()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddDb()
                .Build();
            Console.WriteLine(config.GetSection("key1").Value);
            Console.WriteLine(config.GetSection("key2").Value);
            Console.WriteLine(config.GetSection("key3").Value);
            Console.WriteLine(config.GetSection("key4").Value);
            Console.WriteLine(config.GetSection("key5").Value);
        }

        /// <summary>
        /// Options模式
        /// </summary>
        static void Demo9()
        {

            Dictionary<string, string> source = new Dictionary<string, string>()
            {
                ["source:p1:p1"] = "source:p1:p1",
                ["source:p1:p2"] = "source:p1:p2",
                ["source:p2:p1"] = "source:p2:p1",
                ["source:p2:p2"] = "source:p2:p2",
            };
            IConfigurationRoot config = new ConfigurationBuilder()
              .Add(new MemoryConfigurationSource() { InitialData = source })
              .Build();
            var obj = new ServiceCollection()
                  .AddOptions()
                  .Configure<Obj1>("", config.GetSection(""))
                  .BuildServiceProvider()
                  .GetRequiredService<IOptions<Obj1>>()
                  .Value;

        }


        public class Obj1
        {
            public Obj1_Sub1 p1 { get; set; }
            public Obj1_Sub2 p2 { get; set; }

        }
        public class Obj1_Sub1
        {
            public string p1 { get; set; }
            public string p2 { get; set; }
        }
        public class Obj1_Sub2
        {
            public string p1 { get; set; }
            public string p2 { get; set; }
        }


    }

    class DbSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbProvider();
        }
    }
    class DbProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Data = new Dictionary<string, string>()
            {
                ["key1"] = "value1",
                ["key2"] = "value2",
                ["key3"] = "value3",
                ["key4"] = "value4",
                ["key5"] = "value5",
            };
        }
    }
    public static class DbExtensions
    {
        public static IConfigurationBuilder AddDb(this IConfigurationBuilder builder, string name = null)
        {
            DbSource dbSource = new DbSource();
            builder.Add(dbSource);
            return builder;
        }
    }
}
