using AutoMapper;
using AutoMapper_CJL.ConsoleApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper_CJL.ConsoleApp
{
    //教程https://www.cnblogs.com/gl1573/p/13098031.html
    class Program
    {
        static void Main(string[] args)
        {
            Demo6();
        }
        /// <summary>
        /// 进行简单映射
        /// </summary>
        static void Demo1()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //左边为源右边为转换实体
                cfg.CreateMap<DataEntity, DataDto>();
                cfg.CreateMap<DataDto, DataEntity>();
            }
            );
            //映射处理器
            IMapper mapper = config.CreateMapper();
            //AutoMapper的使用
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
        /// <summary>
        /// 加载配置的方式
        /// </summary>
        static void Demo2()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //直接进行配置,左边为源右边为转换实体
                cfg.CreateMap<DataEntity, DataDto>();
                cfg.CreateMap<DataDto, DataEntity>();
                //基于配置类进行配置
                cfg.AddProfile<CustomProfile>();
                //从程序集中搜索
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
        }
        /// <summary>
        /// 设置源和目标命名约定
        /// </summary>
        void Demo3()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //指定约定规则
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();

                //单词替换
                cfg.ReplaceMemberName("Ä", "A");
                cfg.ReplaceMemberName("í", "i");
                cfg.ReplaceMemberName("Airlina", "Airline");

            });
        }
        /// <summary>
        /// 映射规则配置
        /// </summary>
        static void Demo4()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataDto, DataEntity>()
                    //配置映射,指定目标和源的映射关系
                    .ForMember(dest => dest.Property1, opt => opt.MapFrom(src => src.Property1))
                    //自定义解析器
                    .ForMember(dest => dest.Id, opt => opt.MapFrom<CustomResolver>())
                    //对null的处理
                    .ForMember(dest => dest.Property1, opt => opt.NullSubstitute(""))
                    //条件映射,映射约束
                    .ForMember(dest => dest.Property1, opt => opt.Condition(src => true))
                    //自定义映射
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
            });
            //映射处理器
            IMapper mapper = config.CreateMapper();
        }
        /// <summary>
        /// 开放泛型映射
        /// </summary>
        static void Demo5()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(Source<>), typeof(Destination<>)));
            IMapper mapper = config.CreateMapper();
            var source = new Source<int> { Value = 10 };
            var dest = mapper.Map<Source<int>, Destination<int>>(source);
        }
        /// <summary>
        /// 和依赖注入所结合
        /// </summary>
        static void Demo6()
        {
            var provider = new ServiceCollection()
                .AddAutoMapper(cfg => cfg.CreateMap(typeof(Source<>), typeof(Destination<>)))
                .AddSingleton<DIAutoMapperObj>();
            DIAutoMapperObj dIAutoMapperObj = provider.BuildServiceProvider().GetService<DIAutoMapperObj>();
        }
    }
    public class Source
    {
        public string Value { get; set; }
    }
    public class Destination
    {
        public string Value { get; set; }
    }
    public class Source<T>
    {
        public T Value { get; set; }
    }
    public class Destination<T>
    {
        public T Value { get; set; }
    }
    class DIAutoMapperObj
    {
        private readonly IMapper _mapper;
        public DIAutoMapperObj(IMapper mapper)
        {
            _mapper = mapper;
        }
    }

    public class CustomResolver : IValueResolver<DataDto, DataEntity, string>
    {
        public string Resolve(DataDto source, DataEntity destination, string destMember, ResolutionContext context)
        {
            return source.Property1;
        }
    }
}