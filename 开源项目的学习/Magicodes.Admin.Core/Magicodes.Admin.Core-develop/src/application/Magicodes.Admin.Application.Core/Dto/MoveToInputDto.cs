// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : MoveToInputDto.cs
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
    ///     排序迁移
    /// </summary>
    public class MoveToInputDto<TPrimaryKey>
    {
        /// <summary>
        ///     源Id
        /// </summary>
        public TPrimaryKey SourceId { get; set; }

        /// <summary>
        ///     目标Id
        /// </summary>
        public TPrimaryKey TargetId { get; set; }

        /// <summary>
        ///     移动位置
        /// </summary>
        public MoveToPositions MoveToPosition { get; set; }
    }
}