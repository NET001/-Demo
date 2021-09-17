using AutoMapper;
using AutoMapper_CJL.ConsoleApp.Model;
using System;

namespace AutoMapper_CJL.ConsoleApp
{
    //教程https://www.cnblogs.com/gl1573/p/13098031.html
    class Program
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //配置映射关系
                cfg.CreateMap<DataEntity, DataDto>();
                //指定配置类
                cfg.AddProfile<CustomProfile>();
                //扫描当前的程序集
                cfg.AddMaps(System.AppDomain.CurrentDomain.GetAssemblies());
            }
            );
            var mapper = config.CreateMapper();
            DataDto ddto = new DataDto() { Guid = "123" };
            DataEntity dentity = new DataEntity() { Id = "321" };
            DataDto dto = mapper.Map<DataDto>(dentity);
        }
    }
}
