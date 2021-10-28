using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
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
            Demo2();
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

        static void Demo4()
        {
            Dictionary<string, object> source = new Dictionary<string, object>()
            {
                ["key1"] = "value1",
                ["key2"] = 123,
                ["key3"] = null,
                ["key4"] = new Obj1() { p1=new Obj1_Sub1() { p1="p1p1"} },
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
}
