// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ICommonAppService.cs
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
using System.Threading.Tasks;
using Abp.Application.Services;
using Magicodes.Admin.Application.Common.Dto;

namespace Magicodes.Admin.Application.Common
{
    /// <summary>
    ///     通用服务
    /// </summary>
    public interface ICommonAppService : IApplicationService
    {
        /// <summary>
        ///     获取枚举值列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GetEnumValuesListDto> GetEnumValuesList(GetEnumValuesListInput input);

        /// <summary>
        /// Gets the object files.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        Task<List<GetObjectListDto>> GetObjectFiles(GetObjectInput input);
    }
}