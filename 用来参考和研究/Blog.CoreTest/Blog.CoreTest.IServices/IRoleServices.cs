using Blog.CoreTest.IServices.BASE;
using Blog.CoreTest.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.IServices
{
    public interface IRoleServices : IBaseServices<Role>
    {
        Task<Role> SaveRole(string roleName);
        Task<string> GetRoleNameByRid(int rid);

    }
}
