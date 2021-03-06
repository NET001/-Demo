// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : RemoveObjectAttachmentsInput.cs
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

namespace Magicodes.Admin.Application.Common.Dto
{
    public class RemoveObjectAttachmentsInput
    {
        /// <summary>
        ///     主键Id数组
        /// </summary>
        public long[] Ids { get; set; }

        /// <summary>
        ///     附件类型
        /// </summary>
        public string ObjectType { get; set; }
    }
}