using Blog.Core_CJL.IServices.BASE;
using Blog.Core_CJL.Model.Models;
using Blog.Core_CJL.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.IServices
{
    public interface IBlogArticleServices : IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> GetBlogs();
        Task<BlogViewModels> GetBlogDetails(int id);
    }
}
