// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TreeTableOutputDto.cs
//           description :
// 
//           created by 雪雁 at  2019-10-10 18:12
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

using System.Collections.Generic;

namespace Magicodes.Admin.Application.Core.Dto
{
    /// <summary>
    ///     Tree输出结果
    /// </summary>
    public class TreeOutputDto
    {
        /// <summary>
        ///     数据集合
        /// </summary>
        public ICollection<TreeItemDto> Data { get; set; }
    }
}