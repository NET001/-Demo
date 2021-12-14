using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace StaticFiles_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        /// <summary>
        /// 配置完成后就可以采用URL来获取wwwroot下面的静态文件了
        /// </summary>
        static void Demo1()
        {
            //获取一个默认建造者
            Host.CreateDefaultBuilder()
                //进行构建者的默认实现
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app.UseStaticFiles()))
                .Build()
                .Run();
        }
        /// <summary>
        /// 新增自定义配置
        /// </summary>
        static void Demo2()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app
                    //默认以wwwroot文件
                    .UseStaticFiles()
                    //自定义配置一个doc文件夹
                    .UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "doc")),
                        RequestPath = "/documents"
                    })
                    ))
                .Build()
                .Run();
        }
        /// <summary>
        /// 获取路径结构
        /// </summary>
        static void Demo3()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app
                    //默认以wwwroot文件
                    .UseStaticFiles()
                    //将wwwroot以文件结构以html呈现
                    .UseDirectoryBrowser()
                    //自定义配置一个doc文件夹
                    .UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "doc")),
                        RequestPath = "/documents"
                    })
                    //自定义一个显示指定文件的文件结构
                    .UseDirectoryBrowser(new DirectoryBrowserOptions()
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "doc")),
                        RequestPath = "/documents"
                    })
                    ))
                .Build()
                .Run();

        }
        static void Demo4()
        {
            FileExtensionContentTypeProvider contentTypeProvider = new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings.Add(".img", "image/jpg");
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app
                    //设置识别指定内容的文件
                    .UseStaticFiles(new StaticFileOptions()
                    {
                        ContentTypeProvider = contentTypeProvider
                    })
                ))
                .Build()
                .Run();
        }
    }
}
