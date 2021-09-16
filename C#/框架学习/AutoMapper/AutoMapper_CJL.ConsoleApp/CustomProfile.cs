using AutoMapper;
using AutoMapper_CJL.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper_CJL.ConsoleApp
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<DataEntity, DataDto>();
            CreateMap<DataDto, DataEntity>();
        }
    }
}
