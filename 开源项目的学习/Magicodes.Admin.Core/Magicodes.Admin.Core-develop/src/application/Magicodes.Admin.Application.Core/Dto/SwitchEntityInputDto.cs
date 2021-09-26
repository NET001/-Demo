// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : SwitchEntityInputDto.cs
//           description :
// 
//           created by 雪雁 at  2019-10-10 18:12
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

using Abp.Application.Services.Dto;

namespace Magicodes.Admin.Application.Core.Dto
{
    /// <summary>
    ///     开关输入参数Dto
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class SwitchEntityInputDto<TPrimaryKey> : EntityDto<TPrimaryKey>
    {
        /// <summary>
        ///     开关值（bool类型）
        /// </summary>
        public bool SwitchValue { get; set; }
    }
}