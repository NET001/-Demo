using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.CoreTest.IServices.BASE;
using Blog.CoreTest.Model.Models;

namespace Blog.CoreTest.IServices
{
    public interface ISysUserInfoServices : IBaseServices<sysUserInfo>
    {
        Task<sysUserInfo> SaveUserInfo(string loginName, string loginPwd);
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
    }
}
