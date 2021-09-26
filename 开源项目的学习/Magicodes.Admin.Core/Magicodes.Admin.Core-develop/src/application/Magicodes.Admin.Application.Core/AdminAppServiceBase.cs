// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminAppServiceBase.cs
//           description :
// 
//           created by 雪雁 at  2019-10-14 14:25
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AspNetZeroCore.Net;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.UI;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.Core.Storage;
using Magicodes.Admin.Localization;
using Magicodes.ExporterAndImporter.Core;
using Microsoft.AspNetCore.Identity;

namespace Magicodes.Admin.Application.Core
{
    /// <summary>
    ///     Derive your application services from this class.
    /// </summary>
    public abstract class AdminAppServiceBase : ApplicationService
    {
        /// <summary>
        /// </summary>
        protected AdminAppServiceBase()
        {
            LocalizationSourceName = LocalizationConsts.LocalizationSourceName;
        }

        /// <summary>
        /// </summary>
        public TenantManager TenantManager { get; set; }


        /// <summary>
        /// </summary>
        public IAppFolders AppFolders { get; set; }

        /// <summary>
        /// </summary>
        public UserManager UserManager { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null) throw new Exception("There is no current user!");

            return user;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected virtual User GetCurrentUser()
        {
            return AsyncHelper.RunSync(GetCurrentUserAsync);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="identityResult"></param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        /// <summary>
        ///     权限检查
        /// </summary>
        /// <param name="permissionName"></param>
        protected virtual void CheckPermission(string permissionName)
        {
            if (!string.IsNullOrEmpty(permissionName)) PermissionChecker.Authorize(permissionName);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        protected virtual void ShowUserFriendlyExceptionIfNull<T>(T obj, string message)
        {
            if (obj == null) throw new UserFriendlyException(message);
        }

        /// <summary>
        ///     获取文件路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected string GetFilePathFromTemp(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new UserFriendlyException(L("FileNotAllowedEmpty"));

            if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(AppFolders.TemporaryFolder, filePath);

            return filePath;
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class AdminAppServiceBase<TEntity, TPrimaryKey> : AdminAppServiceBase
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        ///     导入程序
        /// </summary>
        public IImporter Importer { get; set; }

        /// <summary>
        ///     导出程序
        /// </summary>
        public IExporter Exporter { get; set; }


        /// <summary>
        /// </summary>
        public ITempFileCacheManager TempFileCacheManager { get; set; }

        /// <summary>
        ///     导入权限名称
        /// </summary>
        protected virtual string ImportPermissionName { get; set; }

        /// <summary>
        ///     导出权限名称
        /// </summary>
        protected virtual string ExportPermissionName { get; set; }

        /// <summary>
        /// </summary>
        protected virtual string GetPermissionName { get; set; }

        /// <summary>
        /// </summary>
        public IRepository<TEntity, TPrimaryKey> Repository { get; set; }

        /// <summary>
        ///     获取单个数据
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task<TDto> Get<TDto>(IEntityDto<TPrimaryKey> input) where TDto : class, new()
        {
            CheckPermission(GetPermissionName);
            var entity = await Repository.GetAsync(input.Id);
            return ObjectMapper.Map<TDto>(entity);
        }

        /// <summary>
        ///     导入
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="func">导入数据验证加工处理逻辑</param>
        /// <returns></returns>
        protected virtual async Task Import<TDto>(string filePath,
            Func<ICollection<TDto>, ICollection<TEntity>> func = null)
            where TDto : class, new()
        {
            CheckPermission(ImportPermissionName);

            filePath = GetFilePathFromTemp(filePath);

            if (!File.Exists(filePath)) throw new UserFriendlyException(L("FileNotAllowedEmpty"));

            var result = await Importer.Import<TDto>(filePath);
            if (result.HasError) throw new UserFriendlyException(L("HasImportError"));

            var list = func == null ? ObjectMapper.Map<List<TEntity>>(result.Data) : func(result.Data);
            foreach (var entity in list) await Repository.InsertAsync(entity);
            //TODO:批量插入
        }

        /// <summary>
        ///     下载导入错误标注文件
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected virtual async Task<FileDto> DownloadImportErrorFile<TDto>(string filePath)
            where TDto : class, new()
        {
            CheckPermission(ImportPermissionName);
            var fileDto = new FileDto(L(typeof(TEntity).Name) + L("ImportError") + ".xlsx",
                MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);

            filePath = filePath.Replace(Path.GetFileName(filePath),
                $"{Path.GetFileNameWithoutExtension(filePath)}_{Path.GetExtension(filePath)}");
            filePath = GetFilePathFromTemp(filePath);

            var bytes = await File.ReadAllBytesAsync(filePath);
            TempFileCacheManager.SetFile(fileDto.FileToken, bytes);
            return fileDto;
        }

        /// <summary>
        ///     验证导入数据
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected virtual async Task<ImportVerifyOutputDto> VerifyImportData<TDto>(string filePath)
            where TDto : class, new()
        {
            CheckPermission(ImportPermissionName);
            filePath = GetFilePathFromTemp(filePath);

            var result = await Importer.Import<TDto>(filePath);
            return new ImportVerifyOutputDto
            {
                ExceptionMessage = result.Exception?.Message,
                HasError = result.HasError,
                RowErrors = result.RowErrors,
                TemplateErrors = result.TemplateErrors
            };
        }


        /// <summary>
        ///     导出下载模板
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<FileDto> DownloadImportTpl<TDto>()
            where TDto : class, new()
        {
            CheckPermission(ImportPermissionName);

            var bytes = await Importer.GenerateTemplateBytes<TDto>();
            var fileDto = new FileDto(L(typeof(TEntity).Name) + L("ImportTpl") + ".xlsx",
                MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            TempFileCacheManager.SetFile(fileDto.FileToken, bytes);
            return fileDto;
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        protected virtual async Task<FileDto> Export<TDto>(IList<TDto> list)
            where TDto : class, new()
        {
            CheckPermission(ExportPermissionName);

            if (list == null || list.Count == 0) throw new UserFriendlyException(L("NoDataToExport"));

            var fileDto = new FileDto(L(typeof(TEntity).Name) + L("ExportData") + ".xlsx",
                MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var byteArray = await Exporter.ExportAsByteArray(list);
            TempFileCacheManager.SetFile(fileDto.FileToken, byteArray);
            return fileDto;
        }
    }
}