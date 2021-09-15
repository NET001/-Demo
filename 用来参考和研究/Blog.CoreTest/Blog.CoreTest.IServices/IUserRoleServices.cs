using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.CoreTest.IServices.BASE;
using Blog.CoreTest.Model.Models;


namespace Blog.CoreTest.IServices
{
    public interface IUserRoleServices : IBaseServices<UserRole>
    {

        Task<UserRole> SaveUserRole(int uid, int rid);
        Task<int> GetRoleIdByUid(int uid);
    }
}
