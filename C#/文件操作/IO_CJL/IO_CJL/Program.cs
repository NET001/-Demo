using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Reflection;
using System.IO;

namespace IO_CJL
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        /// <summary>
        /// 文件系统
        /// </summary>
        static void Demo1()
        {
            //读取指定物力存档的路径
            IFileProvider physicalFileProvider = new PhysicalFileProvider(@"E:\CJL_学习\Dome\C#\文件操作\IO_CJL");

            //读取嵌入式的文件
            IFileProvider EmbeddedFileProvider = new EmbeddedFileProvider(Assembly.LoadFrom(""));

            //对文件进行监控
            ChangeToken.OnChange(() => physicalFileProvider.Watch(""), () =>
            {
                Console.WriteLine("文件状态发送了变化");
            });
        }
        //获取bin目录
        static void Demo2()
        {
            Directory.GetCurrentDirectory();
        }
    }
}
