using Abp.Web.Models;

namespace Magicodes.Admin.Application.Custom
{
    /// <summary>
    /// 输出测试
    /// </summary>
    public interface IResponseTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        AjaxResponseBase TestOkMessage();
        AjaxResponseBase TestOkResultMessage();
    }
}