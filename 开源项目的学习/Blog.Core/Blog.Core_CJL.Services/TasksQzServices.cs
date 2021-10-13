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
    public partial class TasksQzServices : BaseServices<TasksQz>, ITasksQzServices
    {
        IBaseRepository<TasksQz> _dal;
        public TasksQzServices(IBaseRepository<TasksQz> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
