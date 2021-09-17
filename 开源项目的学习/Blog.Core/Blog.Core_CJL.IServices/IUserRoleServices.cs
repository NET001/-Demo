using Blog.Core_CJL.IServices.BASE;
using Blog.Core_CJL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.IServices
{
    public interface IUserRoleServices : IBaseServices<UserRole>
    {
        Task<UserRole> SaveUserRole(int uid, int rid);
        Task<int> GetRoleIdByUid(int uid);
    }
}
