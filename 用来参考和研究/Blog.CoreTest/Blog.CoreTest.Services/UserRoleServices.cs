using Blog.CoreTest.Common.Attribute;
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

    public class UserRoleServices : BaseServices<UserRole>, IUserRoleServices
    {

        IBaseRepository<UserRole> _dal;
        public UserRoleServices(IBaseRepository<UserRole> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        public async Task<UserRole> SaveUserRole(int uid, int rid)
        {
            UserRole userRole = new UserRole(uid, rid);

            UserRole model = new UserRole();
            var userList = await base.Query(a => a.UserId == userRole.UserId && a.RoleId == userRole.RoleId);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(userRole);
                model = await base.QueryById(id);
            }
            return model;
        }
        [Caching(AbsoluteExpiration = 30)]
        public async Task<int> GetRoleIdByUid(int uid)
        {
            return ((await base.Query(d => d.UserId == uid)).OrderByDescending(d => d.Id).LastOrDefault()?.RoleId).ObjToInt();
        }
    }
}