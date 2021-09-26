// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : CommonAppService.cs
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Reflection.Extensions;
using Magicodes.Admin.Application.Common.Dto;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Attachments;
using Magicodes.Admin.Core.Custom;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Application.Common
{
    /// <summary>
    ///     通用服务
    /// </summary>
    public class CommonAppService : AppServiceBase, ICommonAppService
    {
        private readonly ISettingManager _settingManager;
        private readonly IRepository<ObjectAttachmentInfo, long> _objectAttachmentInfoRepository;


        public CommonAppService(ISettingManager settingManager, IRepository<ObjectAttachmentInfo, long> objectAttachmentInfoRepository)
        {
            _settingManager = settingManager;
            _objectAttachmentInfoRepository = objectAttachmentInfoRepository;
        }

        /// <summary>
        ///     获取枚举值列表
        /// </summary>
        /// <returns></returns>
        public List<GetEnumValuesListDto> GetEnumValuesList(GetEnumValuesListInput input)
        {
            Type type = null;
            //if (input.FullName.Contains("Magicodes.Admin.Core.Custom"))
            if (input.FullName.Contains(".Core.Custom"))
                type = typeof(AppCoreModule).GetAssembly().GetType(input.FullName);
            else
                type = typeof(AdminCoreModule).GetAssembly().GetType(input.FullName);
            //var type = typeof(AdminCoreModule).GetAssembly().GetType(input.FullName);
            if (!type.IsEnum) return null;

            var names = Enum.GetNames(type);
            var values = Enum.GetValues(type);
            var list = new List<GetEnumValuesListDto>();
            var index = 0;
            foreach (var value in values)
            {
                list.Add(new GetEnumValuesListDto
                {
                    DisplayName = L(names[index]),
                    Value = Convert.ToInt32(value)
                });
                index++;
            }

            return list;
        }

        /// <summary>
        /// 获取对象图片列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetObjectListDto>> GetObjectImages(GetObjectInput input)
        {
            var objectType = Enum.Parse<AttachmentObjectTypes>(input.ObjectType);
            var list = await _objectAttachmentInfoRepository.GetAllIncluding(p => p.AttachmentInfo)
                .Where(p => p.ObjectId == input.ObjectId && p.ObjectType == objectType && p.AttachmentInfo.AttachmentType == AttachmentTypes.Image)
                .Select(p => p.AttachmentInfo)
                .ToListAsync();
            return list.MapTo<List<GetObjectListDto>>();
        }

        /// <summary>
        /// 获取对象图片列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<List<GetObjectListDto>> GetObjectFiles(GetObjectInput input)
        {
            var objectType = Enum.Parse<AttachmentObjectTypes>(input.ObjectType);
            var list = await _objectAttachmentInfoRepository.GetAllIncluding(p => p.AttachmentInfo)
                .Where(p => p.ObjectId == input.ObjectId && p.ObjectType == objectType && p.AttachmentInfo.AttachmentType == AttachmentTypes.File)
                .Select(p => p.AttachmentInfo)
                .ToListAsync();
            return list.MapTo<List<GetObjectListDto>>();
        }

        /// <summary>
        /// 移除对象附件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task RemoveObjectAttachments(RemoveObjectAttachmentsInput input)
        {
            var objectType = Enum.Parse<AttachmentObjectTypes>(input.ObjectType);
            await _objectAttachmentInfoRepository.DeleteAsync(p => input.Ids.Contains(p.AttachmentInfoId) && p.ObjectType == objectType);
        }

        /// <summary>
        /// 添加附件关联
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddObjectAttachmentInfos(AddObjectAttachmentInfosInput input)
        {
            var objectType = Enum.Parse<AttachmentObjectTypes>(input.ObjectType);
            var attachmentInfos = await _objectAttachmentInfoRepository.GetAll().Where(p => p.ObjectId == input.ObjectId && p.ObjectType == objectType).ToListAsync();
            var objectAttachmentInfos = input.AttachmentInfoIds.Select(p => new ObjectAttachmentInfo
            {
                ObjectType = objectType,
                ObjectId = input.ObjectId,
                AttachmentInfoId = p
            }).ToList();
            foreach (var objectAttachmentInfo in objectAttachmentInfos)
            {
                if (attachmentInfos == null || attachmentInfos.Count == 0 || (attachmentInfos.All(p => p.AttachmentInfoId != objectAttachmentInfo.AttachmentInfoId)))
                    await _objectAttachmentInfoRepository.InsertAsync(objectAttachmentInfo);
            }
        }
    }
}