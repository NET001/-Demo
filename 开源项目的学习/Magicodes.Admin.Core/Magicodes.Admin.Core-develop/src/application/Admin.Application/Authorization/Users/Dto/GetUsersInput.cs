// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetUsersInput.cs
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
using Abp.Runtime.Validation;
using Magicodes.Admin.Application.Core.Dto;

namespace Magicodes.Admin.Application.Authorization.Users.Dto
{
    public class GetUsersInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary>
        ///     文字搜索
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        ///     权限列表
        /// </summary>
        public List<string> Permission { get; set; }

        /// <summary>
        ///     检索角色Id列表
        /// </summary>
        public List<int> Role { get; set; }
        ///// <summary>
        ///// 是否锁定
        ///// </summary>
        //public bool OnlyLockedUsers { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting)) Sorting = "Name";
        }
    }
}