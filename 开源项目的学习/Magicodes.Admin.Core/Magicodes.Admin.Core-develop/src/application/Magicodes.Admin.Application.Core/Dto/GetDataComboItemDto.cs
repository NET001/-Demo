// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetDataComboItemDto.cs
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
    ///     获取下拉列表
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class GetDataComboItemDto<TPrimaryKey>
    {
        /// <summary>
        ///     显示名（会进行语言处理）
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     值
        /// </summary>
        public TPrimaryKey Value { get; set; }
    }
}