// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : EditionAppService.cs
//           description :
// 
//           created by 雪雁 at  2019-06-17 10:17
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Magicodes.Admin.Application.Core;
using Magicodes.Admin.Application.Editions.Dto;
using Magicodes.Admin.Core.Authorization;
using Magicodes.Admin.Core.Editions;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Application.Editions
{
    [AbpAuthorize(AppPermissions.Pages_Editions)]
    public class EditionAppService : AdminAppServiceBase, IEditionAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<SubscribableEdition> _subscribableEditionRepository;

        public EditionAppService(
            EditionManager editionManager,
            IRepository<SubscribableEdition> subscribableEditionRepository)
        {
            _editionManager = editionManager;
            _subscribableEditionRepository = subscribableEditionRepository;
        }

        public async Task<ListResultDto<EditionListDto>> GetEditions()
        {
            var editions = (await _editionManager.Editions.Cast<SubscribableEdition>().ToListAsync())
                .OrderBy(e => e.MonthlyPrice);
            return new ListResultDto<EditionListDto>(
                ObjectMapper.Map<List<EditionListDto>>(editions)
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Create, AppPermissions.Pages_Editions_Edit)]
        public async Task<GetEditionEditOutput> GetEditionForEdit(NullableIdDto input)
        {
            var features = FeatureManager.GetAll()
                .Where(f => f.Scope.HasFlag(FeatureScopes.Edition));

            EditionEditDto editionEditDto;
            List<NameValue> featureValues;

            if (input.Id.HasValue) //Editing existing edition?
            {
                var edition = await _editionManager.FindByIdAsync(input.Id.Value);
                featureValues = (await _editionManager.GetFeatureValuesAsync(input.Id.Value)).ToList();
                editionEditDto = ObjectMapper.Map<EditionEditDto>(edition);
            }
            else
            {
                editionEditDto = new EditionEditDto();
                featureValues = features.Select(f => new NameValue(f.Name, f.DefaultValue)).ToList();
            }

            var featureDtos = ObjectMapper.Map<List<FlatFeatureDto>>(features).OrderBy(f => f.DisplayName).ToList();

            return new GetEditionEditOutput
            {
                Edition = editionEditDto,
                Features = featureDtos,
                FeatureValues = featureValues.Select(fv => new NameValueDto(fv)).ToList()
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Create, AppPermissions.Pages_Editions_Edit)]
        public async Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input)
        {
            if (!input.Edition.Id.HasValue)
            {
                if (await _subscribableEditionRepository.GetAll().AnyAsync(p => p.Name == input.Edition.DisplayName))
                    throw new UserFriendlyException(L("EditionNameNotAllowedRepeat"));
                await CreateEditionAsync(input);
            }
            else
            {
                await UpdateEditionAsync(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Delete)]
        public async Task DeleteEdition(EntityDto input)
        {
            var edition = await _editionManager.GetByIdAsync(input.Id);
            await _editionManager.DeleteAsync(edition);
        }

        public async Task<List<SubscribableEditionComboboxItemDto>> GetEditionComboboxItems(
            int? selectedEditionId = null, bool addAllItem = false, bool onlyFreeItems = false,
            bool addNotAssignedItem = true)
        {
            var editions = await _editionManager.Editions.ToListAsync();
            var subscribableEditions = editions.Cast<SubscribableEdition>()
                .WhereIf(onlyFreeItems, e => e.IsFree)
                .OrderBy(e => e.MonthlyPrice);

            var editionItems =
                new ListResultDto<SubscribableEditionComboboxItemDto>(subscribableEditions
                    .Select(e => new SubscribableEditionComboboxItemDto(e.Id.ToString(), e.DisplayName, e.IsFree))
                    .ToList()).Items.ToList();

            if (addNotAssignedItem)
            {
                var defaultItem = new SubscribableEditionComboboxItemDto("", L("NotAssigned"), null);
                editionItems.Insert(0, defaultItem);
            }

            if (addAllItem)
                editionItems.Insert(0, new SubscribableEditionComboboxItemDto("-1", "- " + L("All") + " -", null));

            if (selectedEditionId.HasValue)
            {
                var selectedEdition = editionItems.FirstOrDefault(e => e.Value == selectedEditionId.Value.ToString());
                if (selectedEdition != null) selectedEdition.IsSelected = true;
            }
            else
            {
                editionItems[0].IsSelected = true;
            }

            return editionItems;
        }

        /// <summary>
        ///     批量删除
        /// </summary>
        /// <param name="input">要删除的集合</param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Editions_BatchDelete)]
        public async Task BatchDelete(List<EntityDto> input)
        {
            foreach (var entity in input)
            {
                var edition = await _editionManager.GetByIdAsync(entity.Id);
                await _editionManager.DeleteAsync(edition);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Create)]
        protected virtual async Task CreateEditionAsync(CreateOrUpdateEditionDto input)
        {
            //TODO：此处是否要控制版本名不能一样
            var edition = ObjectMapper.Map<SubscribableEdition>(input.Edition);

            edition.Name = edition.DisplayName;

            if (edition.ExpiringEditionId.HasValue)
            {
                var expiringEdition =
                    (SubscribableEdition) await _editionManager.GetByIdAsync(edition.ExpiringEditionId.Value);
                if (!expiringEdition.IsFree) throw new UserFriendlyException(L("ExpiringEditionMustBeAFreeEdition"));
            }

            await _editionManager.CreateAsync(edition);
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the edition.

            //TODO：为了兼容新版UI暂时加的判断
            if (input.FeatureValues != null && input.FeatureValues.Count > 0)
                await SetFeatureValues(edition, input.FeatureValues);
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Edit)]
        protected virtual async Task UpdateEditionAsync(CreateOrUpdateEditionDto input)
        {
            if (input.Edition.Id != null)
            {
                var edition = await _editionManager.GetByIdAsync(input.Edition.Id.Value);

                var existingSubscribableEdition = (SubscribableEdition) edition;
                var updatingSubscribableEdition = ObjectMapper.Map<SubscribableEdition>(input.Edition);
                if (existingSubscribableEdition.IsFree &&
                    !updatingSubscribableEdition.IsFree &&
                    await _subscribableEditionRepository.CountAsync(e =>
                        e.ExpiringEditionId == existingSubscribableEdition.Id) > 0)
                    throw new UserFriendlyException(L("ThisEditionIsUsedAsAnExpiringEdition"));

                ObjectMapper.Map(input.Edition, edition);

                //TODO：为了兼容新版UI暂时加的判断
                if (input.FeatureValues != null && input.FeatureValues.Count > 0)
                    await SetFeatureValues(edition, input.FeatureValues);
            }
        }

        private Task SetFeatureValues(Edition edition, List<NameValueDto> featureValues)
        {
            return _editionManager.SetFeatureValuesAsync(edition.Id,
                featureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }
    }
}