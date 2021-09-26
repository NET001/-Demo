using Abp.Web.Models;
using Magicodes.Admin.Core.Responses;
using System.Collections.Generic;
using Magicodes.Admin.Application.Core;

namespace Magicodes.Admin.Application.Custom
{
    /// <summary>
    /// 输出结果测试
    /// </summary>
    public class ResponseTest : AdminAppServiceBase, IResponseTest
    {
        /// <summary>
        /// 返回成功消息和数据
        /// </summary>
        /// <returns></returns>
        public AjaxResponseBase TestOkResultMessage() => this.Ok(new List<string>() { "测试1", "测试1" }, "操作成功！");

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <returns></returns>
        public AjaxResponseBase TestOkMessage() => this.Ok("操作成功！");
    }
}
