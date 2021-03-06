using Blog.CoreTest.IServices.BASE;
using Blog.CoreTest.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.CoreTest.IServices
{
    public interface IRoleModulePermissionServices : IBaseServices<RoleModulePermission>
    {
        /// <summary>
        /// 获得角色模块
        /// </summary>
        /// <returns></returns>
        Task<List<RoleModulePermission>> GetRoleModule();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<TestMuchTableResult>> QueryMuchTable();
        Task<List<RoleModulePermission>> RoleModuleMaps();
        Task<List<RoleModulePermission>> GetRMPMaps();
        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        Task UpdateModuleId(int permissionId, int moduleId);
    }
}
