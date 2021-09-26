using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Magicodes.Admin.Application.Core.Dto;

namespace Magicodes.Admin.Application.Core
{
    /// <summary>
    /// 导出接口
    /// </summary>
    public interface IExport<in TGetAllInput> where TGetAllInput : IPagedAndSortedResultRequest
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FileDto> GetExport(TGetAllInput input);
    }
}
