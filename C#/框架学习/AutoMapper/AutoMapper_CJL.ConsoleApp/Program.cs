using AutoMapper;
using AutoMapper_CJL.ConsoleApp.Model;
using System;
using System.Collections.Generic;

namespace AutoMapper_CJL.ConsoleApp
{
    //教程https://www.cnblogs.com/gl1573/p/13098031.html
    class Program
    {

        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
                {
                    //配置转换规则
                    cfg.CreateMap<DataDto, DataEntity>()
                       .ConvertUsing((s, j) =>
                       {
                           DataEntity result = new DataEntity();
                           int property1 = 0;
                           double property2 = 0;
                           float property4 = 0.0f;
                           decimal property5 = 0;
                           DateTime datetime = DateTime.Now;
                           result.Id = s.Guid;
                           result.Property1 = int.TryParse(s.Property1, out property1) ? property1 : property1;
                           result.Property2 = double.TryParse(s.Property2, out property2) ? property2 : property2;
                           result.Property3 = s.Property3.ToString();
                           result.Property4 = float.TryParse(s.Property4, out property4) ? property4 : property4;
                           result.Property5 = decimal.TryParse(s.Property5, out property5) ? property5 : property5;
                           result.DateTime = DateTime.TryParse(s.DateTime, out datetime) ? datetime : datetime;
                           return result;
                       });
                    cfg.CreateMap<DataEntity, DataDto>()
                    .ConvertUsing((s, j) =>
                    {
                        DataDto result = new DataDto();
                        result.Guid = s.Id;
                        result.Property1 = s.Property1.ToString();
                        result.Property2 = s.Property1.ToString();
                        result.Property3 = s.Property3.ToString();
                        result.Property4 = s.Property1.ToString();
                        result.Property5 = s.Property1.ToString();
                        result.DateTime = s.DateTime.ToString();
                        return result;
                    });

                }
            );
            var mapper = config.CreateMapper();
            DataDto ddto = new DataDto()
            {
                Guid = "123",
                DateTime = "2021-01-01",
                Property1 = "属性值1",
                Property2 = "属性值2",
                Property3 = "属性值3",
                Property4 = "属性值4",
                Property5 = "属性值5"

            };
            DataEntity dentity = new DataEntity()
            {
                Id = "321",
                DateTime = DateTime.Now,
                Property1 = 10,
                Property2 = 2.123,
                Property3 = new List<int>() { 1, 2, 3, 4, 5, 6 },
                Property4 = 3.11f,
                Property5 = 123
            };
            DataDto dto1 = mapper.Map<DataDto>(dentity);
            DataEntity dto2 = mapper.Map<DataEntity>(ddto);
        }
    }
}
