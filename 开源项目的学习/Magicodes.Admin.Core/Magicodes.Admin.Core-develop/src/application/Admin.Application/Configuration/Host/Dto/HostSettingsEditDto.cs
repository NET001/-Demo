// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : HostSettingsEditDto.cs
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

using System.ComponentModel.DataAnnotations;

namespace Magicodes.Admin.Application.Configuration.Host.Dto
{
    public class HostSettingsEditDto
    {
        [Required] public GeneralSettingsEditDto General { get; set; }

        [Required] public HostUserManagementSettingsEditDto UserManagement { get; set; }

        [Required] public EmailSettingsEditDto Email { get; set; }

        [Required] public TenantManagementSettingsEditDto TenantManagement { get; set; }

        [Required] public SecuritySettingsEditDto Security { get; set; }

        public HostBillingSettingsEditDto Billing { get; set; }
    }
}