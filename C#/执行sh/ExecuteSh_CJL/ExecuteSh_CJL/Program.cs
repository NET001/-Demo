using System;
using System.Diagnostics;

namespace ExecuteSh_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        
        private static void StartProcess()
        {
            string fileName = System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.AppDomain.CurrentDomain.BaseDirectory).FullName).FullName, "IoTCenterWeb/shell");
            Console.WriteLine("StartProcess的fileName" + fileName);
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    fileName = System.IO.Path.Combine(fileName, "restart.bat");
            //}
            //else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            //{
            fileName = System.IO.Path.Combine(fileName, "GWYanChenWater.sh");
            Console.WriteLine("StartProcess的fileName" + fileName);

            //}
            //else
            //{
            //    //fileName += "restart.sh";
            //    fileName = System.IO.Path.Combine(fileName, "restart.sh");
            //}
            //创建一个ProcessStartInfo对象 使用系统shell 指定命令和参数 设置标准输出
            var psi = new ProcessStartInfo(fileName) { RedirectStandardOutput = true };
            try
            {
                //启动
                var proc = Process.Start(psi);
                if (proc == null)
                {
                    Console.WriteLine("StartProcess:无法获取执行程序");
                }
                else
                {
                    Console.WriteLine("StartProcess:启动执行程序，开始获取输出内容");
                    //开始读取
                    using (var sr = proc.StandardOutput)
                    {
                        while (!sr.EndOfStream)
                        {
                            Console.WriteLine(sr.ReadLine());
                        }
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }
                    Console.WriteLine("StartProcess:结束获取输出");
                    Console.WriteLine($"StartProcess:Exited Code ： {proc.ExitCode}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
