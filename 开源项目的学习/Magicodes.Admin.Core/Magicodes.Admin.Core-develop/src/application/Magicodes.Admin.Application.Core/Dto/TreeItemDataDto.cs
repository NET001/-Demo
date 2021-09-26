// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TreeItemDataDto.cs
//           description :
// 
//           created by 雪雁 at  2019-10-10 18:12
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

namespace Magicodes.Admin.Application.Core.Dto
{
    /// <summary>
    ///     Tree每一项数据类型定义
    /// </summary>
    public class TreeItemDataDto
    {
        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     主键Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }
    }
}