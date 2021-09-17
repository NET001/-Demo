using Blog.Core_CJL.IServices.BASE;
using Blog.Core_CJL.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.IServices
{

    public interface ISysUserInfoServices : IBaseServices<sysUserInfo>
    {
        Task<sysUserInfo> SaveUserInfo(string loginName, string loginPwd);
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
    }
}
