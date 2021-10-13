using AutoMapper;
using Blog.Core_CJL.Extensions.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Core_CJL.Extensions.ServiceExtensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //像automapper中添加map配置
            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
        }
    }
}
