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
    public class PermissionServices : BaseServices<Permission>, IPermissionServices
    {
        IBaseRepository<Permission> _dal;
        public PermissionServices(IBaseRepository<Permission> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
