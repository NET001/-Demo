using Blog.Core_CJL.IServices.BASE;
using Blog.Core_CJL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.IServices
{

    public interface IRoleServices : IBaseServices<Role>
    {
        Task<Role> SaveRole(string roleName);
        Task<string> GetRoleNameByRid(int rid);

    }
}
