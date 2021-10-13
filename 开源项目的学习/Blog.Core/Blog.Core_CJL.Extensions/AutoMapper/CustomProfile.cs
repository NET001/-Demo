using AutoMapper;
using Blog.Core_CJL.Model.Models;
using Blog.Core_CJL.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<BlogArticle, BlogViewModels>();
            CreateMap<BlogViewModels, BlogArticle>();
        }
    }
}
