using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            IMemoryCache
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(scvs=>scvs.)
                    .Configure(app => app.Run(async (HttpContext httpContext) =>
                    {

                    })))
                .Build()
                .Run();
        }
    }
}
