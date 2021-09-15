using Blog.CoreTest.IServices;
using Blog.CoreTest.Model.Models;
using Blog.CoreTest.Repository.BASE;
using Blog.CoreTest.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Services
{
    public class ModuleServices : BaseServices<Modules>, IModuleServices
    {
        IBaseRepository<Modules> _dal;
        public ModuleServices(IBaseRepository<Modules> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}