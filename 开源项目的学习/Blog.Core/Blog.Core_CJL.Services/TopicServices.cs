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
    public class TopicServices : BaseServices<Topic>, ITopicServices
    {

        IBaseRepository<Topic> _dal;
        public TopicServices(IBaseRepository<Topic> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 获取开Bug专题分类（缓存）
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 60)]
        public async Task<List<Topic>> GetTopics()
        {
            return await base.Query(a => !a.tIsDelete && a.tSectendDetail == "tbug");
        }

    }
}
