using Blog.Core_CJL.Common.Attribute;
using Blog.Core_CJL.IServices;
using Blog.Core_CJL.Model.Models;
using Blog.Core_CJL.Repository;
using Blog.Core_CJL.Repository.BASE;
using Blog.Core_CJL.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Services
{
    //baseserver中封装好了基础数据库仓促操作方法
    public class RoleModulePermissionServices : BaseServices<RoleModulePermission>, IRoleModulePermissionServices
    {
        readonly IRoleModulePermissionRepository _dal;
        readonly IBaseRepository<Modules> _moduleRepository;
        readonly IBaseRepository<Role> _roleRepository;
        // 将多个仓储接口注入
        public RoleModulePermissionServices(
            IRoleModulePermissionRepository dal,
            IBaseRepository<Modules> moduleRepository,
            IBaseRepository<Role> roleRepository)
        {
            this._dal = dal;
            this._moduleRepository = moduleRepository;
            this._roleRepository = roleRepository;
            base.BaseDal =dal;
        }
        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermissions = await base.Query(a => a.IsDeleted == false);
            var roles = await _roleRepository.Query(a => a.IsDeleted == false);
            var modules = await _moduleRepository.Query(a => a.IsDeleted == false);
            if (roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = roles.FirstOrDefault(d => d.Id == item.RoleId);
                    item.Module = modules.FirstOrDefault(d => d.Id == item.ModuleId);
                }
            }
            return roleModulePermissions;
        }
        public async Task<List<TestMuchTableResult>> QueryMuchTable()
        {
            return await _dal.QueryMuchTable();
        }
        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await _dal.RoleModuleMaps();
        }
        public async Task<List<RoleModulePermission>> GetRMPMaps()
        {
            return await _dal.GetRMPMaps();
        }
        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        public async Task UpdateModuleId(int permissionId, int moduleId)
        {
            await _dal.UpdateModuleId(permissionId, moduleId);
        }
    }
}
