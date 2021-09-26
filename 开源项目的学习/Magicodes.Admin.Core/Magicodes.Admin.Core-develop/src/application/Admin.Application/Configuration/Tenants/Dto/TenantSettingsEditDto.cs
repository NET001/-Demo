// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TenantSettingsEditDto.cs
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
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using Abp.Timing;
using Magicodes.Admin.Application.Configuration.Host.Dto;

namespace Magicodes.Admin.Application.Configuration.Tenants.Dto
{
    public class TenantSettingsEditDto
    {
        public GeneralSettingsEditDto General { get; set; }

        [Required] public TenantUserManagementSettingsEditDto UserManagement { get; set; }

        public EmailSettingsEditDto Email { get; set; }

        [Required] public SecuritySettingsEditDto Security { get; set; }

        public TenantBillingSettingsEditDto Billing { get; set; }


        /// <summary>
        ///     This validation is done for single-tenant applications.
        ///     Because, these settings can only be set by tenant in a single-tenant application.
        /// </summary>
        public void ValidateHostSettings()
        {
            var validationErrors = new List<ValidationResult>();
            if (Clock.SupportsMultipleTimezone && General == null)
                validationErrors.Add(new ValidationResult("General settings can not be null", new[] {"General"}));

            if (Email == null)
                validationErrors.Add(new ValidationResult("Email settings can not be null", new[] {"Email"}));

            if (validationErrors.Count > 0)
                throw new AbpValidationException("Method arguments are not valid! See ValidationErrors for details.",
                    validationErrors);
        }
    }
}