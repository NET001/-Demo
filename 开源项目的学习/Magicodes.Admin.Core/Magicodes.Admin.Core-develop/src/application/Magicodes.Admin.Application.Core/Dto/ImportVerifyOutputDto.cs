using System;
using System.Collections.Generic;
using Magicodes.ExporterAndImporter.Core.Models;

namespace Magicodes.Admin.Application.Core.Dto
{
    /// <summary>
    /// 导入验证结果
    /// </summary>
    public class ImportVerifyOutputDto
    {
        /// <summary>验证错误</summary>
        public virtual IList<DataRowErrorInfo> RowErrors { get; set; }

        /// <summary>模板错误</summary>
        public virtual IList<TemplateErrorInfo> TemplateErrors { get; set; }

        /// <summary>异常信息</summary>
        public virtual string ExceptionMessage { get; set; }

        /// <summary>是否存在导入错误</summary>
        public virtual bool HasError { get; set; }
    }
}
