using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Error_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    //配置上这个中间件就会显示错误信息默认不会显示
                    .UseDeveloperExceptionPage()
                    .Run(context => Task.FromException(new Exception("出现了错误")))
                    ))
                .Build()
                .Run();
        }
    }
}
