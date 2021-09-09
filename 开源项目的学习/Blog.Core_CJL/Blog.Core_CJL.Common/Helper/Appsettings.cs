using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Blog.Core_CJL.Common.Helper
{
    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class Appsettings
    {
        //这个接口专门用于对appsettings.json进行操作
        static IConfiguration Configuration { get; set; }
        static string contentPath { get; set; }
        public Appsettings(string contentPath)
        {
            string Path = "appsettings.json";
            Configuration = new ConfigurationBuilder()
                //从根目录下寻找指定路径
                .SetBasePath(contentPath)
                //添加一个新的配置源。这样做的目的是跨域直接读取项目中的json文件，而不是bin的
                .Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true })
                //构建
                .Build();
        }
        //直接获取的方式是通过依赖注入
        public Appsettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //params的用法是让数据可以为单个的值
        //获取配置文件的值
        public static string app(params string[] sections)
        {
            try
            {
                //判断数组中是否有元素
                if (sections.Any())
                {
                    //构造出指定格式路径获取appsettings.json中的内容
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception) { }
            return "";
        }
        //递归获取配置信息
        public static List<T> app<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            // 引用 Microsoft.Extensions.Configuration.Binder 包
            // 在InitQ中有这个包直接引用InitQ就可以使用了
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }
    }
}
