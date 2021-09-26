// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : CustomCrudeServiceBase.cs
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
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AspNetZeroCore.Net;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityHistory;
using Abp.UI;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Storage;
using Magicodes.Admin.Localization;
using Magicodes.ExporterAndImporter.Core;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Application.Core
{
    /// <summary>
    ///     自定义增删查改导出服务基类
    ///     增
    ///     删
    ///     改
    ///     查
    ///     导出
    /// </summary>
    /// <typeparam name="TEntity">实体模型</typeparam>
    /// <typeparam name="TEntityDto">实体列表Dto</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    /// <typeparam name="TGetAllInput">查询所有Input</typeparam>
    /// <typeparam name="TCreateInput">创建Input</typeparam>
    /// <typeparam name="TUpdateInput">修改Input</typeparam>
    /// <typeparam name="TExportDto">导出Dto</typeparam>
    [AbpAuthorize]
    public abstract class CustomCrudeServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput,
        TUpdateInput, TExportDto> : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput,
        TUpdateInput>, IExport<TGetAllInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TCreateInput : class
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetAllInput : IPagedAndSortedResultRequest
        where TExportDto : class

    {
        /// <summary>
        /// </summary>
        /// <param name="repository"></param>
        protected CustomCrudeServiceBase(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
            this.LocalizationSourceName = LocalizationConsts.LocalizationSourceName;
        }

        /// <summary>
        ///     目录
        /// </summary>
        public IAppFolders AppFolders { get; set; }

        /// <summary>
        ///     导出权限
        /// </summary>
        public string ExportPermissionName { get; set; }

        /// <summary>
        ///     Excel导出注入
        /// </summary>
        public IExporter ExcelExporter { get; set; }

        /// <summary>
        ///     临时文件管理器
        /// </summary>
        public ITempFileCacheManager TempFileCacheManager { get; set; }

        /// <summary>
        ///     导出Excel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UseCase(Description = "Export")]
        public virtual async Task<FileDto> GetExport(TGetAllInput input)
        {
            CheckPermission(ExportPermissionName);
            List<TExportDto> exportData = null;
            var query = CreateFilteredQuery(input);
            var results = await query
                .OrderBy(input.Sorting)
                .ToListAsync();

            exportData = results.Select(MapToExportDto).ToList();

            if (exportData.Count == 0)
            {
                throw new UserFriendlyException(L("NoDataToExport"));
            }

            var fileDto = new FileDto(L(typeof(TEntity).Name) + L("ExportData") + ".xlsx",
                MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var byteArray = await ExcelExporter.ExportAsByteArray(exportData);
            TempFileCacheManager.SetFile(fileDto.FileToken, byteArray);
            return fileDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual TExportDto MapToExportDto(TEntity entity)
        {
            return ObjectMapper.Map<TExportDto>(entity);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UseCase(Description = "Create")]
        public override Task<TEntityDto> Create(TCreateInput input) => base.Create(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UseCase(Description = "Delete")]
        public override Task Delete(EntityDto<TPrimaryKey> input) => base.Delete(input);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<PagedResultDto<TEntityDto>> GetAll(TGetAllInput input) => base.GetAll(input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UseCase(Description = "Update")]
        public override Task<TEntityDto> Update(TUpdateInput input) => base.Update(input);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<TEntityDto> Get(EntityDto<TPrimaryKey> input) => base.Get(input);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="input">The input.</param>
        /// <autogeneratedoc />
        [UseCase(Description = "Deletes")]
        public async Task Deletes(List<TPrimaryKey> input)
        {
            CheckPermission(DeletePermissionName);

            foreach (var id in input)
            {
                await Repository.DeleteAsync(id);
            }
        }
    }
}