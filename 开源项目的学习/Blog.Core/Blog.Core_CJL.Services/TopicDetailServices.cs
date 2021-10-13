using Blog.Core_CJL.Common.Attribute;
using Blog.Core_CJL.IServices;
using Blog.Core_CJL.Model.Models;
using Blog.Core_CJL.Repository.BASE;
using Blog.Core_CJL.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Services
{
    public class TopicDetailServices : BaseServices<TopicDetail>, ITopicDetailServices
    {
        IBaseRepository<TopicDetail> _dal;
        public TopicDetailServices(IBaseRepository<TopicDetail> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 获取开Bug数据（缓存）
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<TopicDetail>> GetTopicDetails()
        {
            return await base.Query(a => !a.tdIsDelete && a.tdSectendDetail == "tbug");
        }
    }
}
