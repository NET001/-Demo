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
            new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomProfile>();
            });
            DataDto ddto = new DataDto() { Guid = "123" };
            DataEntity dentity = new DataEntity() { Id = "321" };
        }
    }
}
