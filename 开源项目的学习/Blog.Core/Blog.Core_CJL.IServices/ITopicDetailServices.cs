using Blog.Core_CJL.IServices.BASE;
using Blog.Core_CJL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.IServices
{
    public interface ITopicDetailServices : IBaseServices<TopicDetail>
    {
        Task<List<TopicDetail>> GetTopicDetails();
    }
}
