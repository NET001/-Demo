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
            Demo1();
        }
        /// <summary>
        /// 进行简单映射
        /// </summary>
        static void Demo1()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    //左边为源右边为转换实体
                    cfg.CreateMap<Source, Destination>();
                    cfg.CreateMap<Destination, Source>();
                }
            );
            //映射处理器
            IMapper mapper = config.CreateMapper();
            //AutoMapper的使用
            Source source = new Source()
            {
                Value = "SourceValue"
            };
            Destination destination = new Destination()
            {
                Value = "DestinationValue"
            };
            Destination o1 = mapper.Map<Destination>(source);
            Source o2 = mapper.Map<Source>(destination);
        }
        /// <summary>
        /// 加载配置的方式
        /// </summary>
        static void Demo2()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
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
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataDto, DataEntity>()
                    //配置映射,指定目标和源的映射关系
                    .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Id))
                    //自定义解析器
                    .ForMember(dest => dest.Guid, opt => opt.MapFrom<CustomResolver>())
                    //对null的处理
                    .ForMember(dest => dest.Property1, opt => opt.NullSubstitute("0"))
                    //条件映射,映射约束
                    .ForMember(dest => dest.Property1, opt => opt.Condition(src => src.Property1 != null))
                    //自定义映射
                    .ConvertUsing((s, j) =>
                    {
                        DataEntity result = new DataEntity();
                        result.Guid = s.Id;
                        result.Property1 = 0;
                        result.Property2 = 0.0f;

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
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(Source<>), typeof(Destination<>)));
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
                //以服务注册的方式进行配置
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
            return source.Id;
        }
    }
}