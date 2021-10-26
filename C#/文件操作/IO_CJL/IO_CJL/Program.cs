using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Reflection;

namespace IO_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //读取指定物力存档的路径
            IFileProvider physicalFileProvider = new PhysicalFileProvider(@"E:\CJL_学习\Dome\C#\文件操作\IO_CJL");

            //读取嵌入式的文件
            IFileProvider EmbeddedFileProvider = new EmbeddedFileProvider(Assembly.LoadFrom(""));

            //对文件进行监控
            ChangeToken.OnChange(() => physicalFileProvider.Watch(""), () => {
                Console.WriteLine("文件状态发送了变化");
            });

            

        }
    }
}
