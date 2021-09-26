// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TreeTableRowDto.cs
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
    ///     Tree Table行数据
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TreeTableRowDto<TEntity> where TEntity : class
    {
        /// <summary>
        ///     数据
        /// </summary>
        public TEntity Data { get; set; }

        /// <summary>
        ///     子集
        /// </summary>
        public ICollection<TreeTableRowDto<TEntity>> Children { get; set; }
    }
}