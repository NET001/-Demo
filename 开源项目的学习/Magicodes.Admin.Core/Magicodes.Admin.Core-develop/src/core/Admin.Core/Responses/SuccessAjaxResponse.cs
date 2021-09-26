using Abp.Web.Models;

namespace Magicodes.Admin.Core.Responses
{
    /// <summary>
    /// Ajax成功结果
    /// </summary>
    public class SuccessAjaxResponse : AjaxResponseBase
    {
        /// <summary>
        /// 处理成功时的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SuccessAjaxResponse(string message) : base()
        {
            Message = message;
            Success = true;
        }
    }

    /// <summary>
    /// Ajax成功结果
    /// </summary>
    public class SuccessAjaxResponse<TResult> : AjaxResponse<TResult>
    {
        /// <summary>
        /// 处理成功时的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SuccessAjaxResponse(string message) : base() => Message = message;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public SuccessAjaxResponse(string message, TResult result)
        {
            Success = true;
            Message = message;
            Result = result;
        }
    }
}
