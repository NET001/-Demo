// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : RoleAppService.cs
//           description :
// 
//           created by 雪雁 at  2019-06-14 11:22
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetZeroCore.Net;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper;
using Magicodes.Admin.Application.Authorization.Permissions;
using Magicodes.Admin.Application.Authorization.Permissions.Dto;
using Magicodes.Admin.Application.Authorization.Roles.Dto;
using Magicodes.Admin.Application.Core;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core.Authorization;
using Magicodes.Admin.Core.Authorization.Roles;
using Magicodes.Admin.Core.Storage;
using Magicodes.ExporterAndImporter.Core;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Application.Authorization.Roles
{
    /// <summary>
    ///     Application service that is used by 'role management' page.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_Roles)]
    public class RoleAppService : AdminAppServiceBase, IRoleAppService
    {
        private readonly IExporter _excelExporter;
        private readonly RoleManager _roleManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public RoleAppService(RoleManager roleManager, IExporter excelExporter
            , ITempFileCacheManager tempFileCacheManager)
        {
            _roleManager = roleManager;

            _tempFileCacheManager = tempFileCacheManager;
            _excelExporter = excelExporter;
        }

        public async Task<PagedResultDto<RoleListDto>> GetRoles(GetRolesInput input)
        {
            var query = _roleManager.Roles
                .WhereIf(
                    !input.FilterText.IsNullOrWhiteSpace(),
                    r =>
                        r.Name.Contains(input.FilterText) ||
                        r.DisplayName.Contains(input.FilterText)
                )
                .WhereIf(
                    input.PermissionNames != null && input.PermissionNames.Count > 0,
                    r => r.Permissions.Any(rp => input.PermissionNames.Contains(rp.Name) && rp.IsGranted)
                );

            var count = await query.CountAsync();

            var roles = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<RoleListDto>(count, ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Create, AppPermissions.Pages_Administration_Roles_Edit)]
        public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = new Permission[0];
            RoleEditDto roleEditDto;

            if (input.Id.HasValue) //Editing existing role?
            {
                var role = await _roleManager.GetRoleByIdAsync(input.Id.Value);
                grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                roleEditDto = ObjectMapper.Map<RoleEditDto>(role);
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName)
                    .ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        public async Task CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        {
            if (input.Role.Id.HasValue)
                await UpdateRoleAsync(input);
            else
                await CreateRoleAsync(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Delete)]
        public async Task DeleteRole(EntityDto input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            var users = await UserManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in users) CheckErrors(await UserManager.RemoveFromRoleAsync(user, role.Name));

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        /// <summary>
        ///     获取所有角色列表
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<RoleListDto>> GetAllRoles()
        {
            var query = await _roleManager.Roles.ToListAsync();
            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(query));
        }

        /// <summary>
        ///     批量删除
        /// </summary>
        /// <param name="input">要删除的集合</param>
        /// <returns></returns>
        public async Task BatchDelete(List<EntityDto> input)
        {
            foreach (var entity in input) await DeleteRole(entity);
        }

        /// <summary>
        ///     export Role to excel
        /// </summary>
        /// <param name="input">query parameter</param>
        /// <returns></returns>
        public async Task<FileDto> GetRolesToExcel(GetRolesInput input)
        {
            async Task<List<RoleExportDto>> getListFunc(bool isLoadSoftDeleteData)
            {
                var query = _roleManager.Roles
                    .WhereIf(
                        !input.FilterText.IsNullOrWhiteSpace(),
                        r =>
                            r.Name.Contains(input.FilterText) ||
                            r.DisplayName.Contains(input.FilterText)
                    )
                    .WhereIf(
                        input.PermissionNames != null && input.PermissionNames.Count > 0,
                        r => r.Permissions.Any(rp => input.PermissionNames.Contains(rp.Name) && rp.IsGranted)
                    );
                var results = await query
                    .OrderBy(input.Sorting)
                    .ToListAsync();

                var exportListDtos = results.MapTo<List<RoleExportDto>>();
                if (exportListDtos.Count == 0) throw new UserFriendlyException(L("NoDataToExport"));

                return exportListDtos;
            }

            List<RoleExportDto> exportData = null;

            exportData = await getListFunc(false);
            var fileDto = new FileDto(L("Role") + L("ExportData") + ".xlsx",
                MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var byteArray = await _excelExporter.ExportAsByteArray(exportData);
            _tempFileCacheManager.SetFile(fileDto.FileToken, byteArray);
            return fileDto;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Edit)]
        protected virtual async Task UpdateRoleAsync(CreateOrUpdateRoleInput input)
        {
            Debug.Assert(input.Role.Id != null, "input.Role.Id should be set.");

            var role = await _roleManager.GetRoleByIdAsync(input.Role.Id.Value);
            role.DisplayName = input.Role.DisplayName;
            role.IsDefault = input.Role.IsDefault;

            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Create)]
        protected virtual async Task CreateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var role = new Role(AbpSession.TenantId, input.Role.DisplayName) {IsDefault = input.Role.IsDefault};
            CheckErrors(await _roleManager.CreateAsync(role));
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the role.
            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }

        private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
        {
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(grantedPermissionNames);
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }
    }
}