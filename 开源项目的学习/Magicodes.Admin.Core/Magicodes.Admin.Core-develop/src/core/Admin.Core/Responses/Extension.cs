using Abp.Application.Services;
using Abp.Web.Models;

namespace Magicodes.Admin.Core.Responses
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 成功返回
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="applicationService"></param>
        /// <param name="result">结果</param>
        /// <param name="message">成功消息</param>
        /// <returns></returns>
        public static AjaxResponseBase Ok<TResult>(this IApplicationService applicationService, TResult result, string message) => new SuccessAjaxResponse<TResult>(message, result);

        public static AjaxResponseBase Ok(this IApplicationService applicationService, string message) => new SuccessAjaxResponse(message);
    }
}
