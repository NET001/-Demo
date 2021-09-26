// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ITreeAppService.cs
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

using System.Threading.Tasks;
using Abp.Application.Services;
using Magicodes.Admin.Application.Core.Dto;
using GetTreeNodesInputDto = Magicodes.Admin.Application.AppCommon.Dto.GetTreeNodesInputDto;

namespace Magicodes.Admin.Application.AppCommon
{
    public interface ITreeAppService : IApplicationService
    {
        /// <summary>
        ///     获取栏目 树级列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TreeOutputDto> GetColumnInfoTreeNodes(GetTreeNodesInputDto input);
    }
}