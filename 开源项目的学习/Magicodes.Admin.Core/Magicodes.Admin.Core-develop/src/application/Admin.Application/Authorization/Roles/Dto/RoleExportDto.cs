// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : RoleExportDto.cs
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

using System;
using Abp.AutoMapper;
using Magicodes.Admin.Core.Authorization.Roles;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Magicodes.Admin.Application.Authorization.Roles.Dto
{
    [ExcelExporter(Name = "问题信息", TableStyle = "Light10")]
    [AutoMapFrom(typeof(Role))]
    public class RoleExportDto
    {
        /// <summary>
        ///     角色名称
        /// </summary>
        [ExporterHeader(DisplayName = "角色名称", IsAutoFit = true)]
        public string Name { get; set; }

        [ExporterHeader(DisplayName = "显示名称", IsAutoFit = true)]
        public string DisplayName { get; set; }

        [ExporterHeader(DisplayName = "是否静态", IsAutoFit = true)]
        public bool IsStatic { get; set; }

        [ExporterHeader(DisplayName = "是否默认", IsAutoFit = true)]
        public bool IsDefault { get; set; }

        [ExporterHeader(Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime CreationTime { get; set; }
    }
}