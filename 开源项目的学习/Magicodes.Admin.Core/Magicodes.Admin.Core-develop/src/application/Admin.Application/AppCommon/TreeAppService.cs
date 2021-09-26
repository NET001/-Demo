// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TreeAppService.cs
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

using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core.Contents;
using GetTreeNodesInputDto = Magicodes.Admin.Application.AppCommon.Dto.GetTreeNodesInputDto;

namespace Magicodes.Admin.Application.AppCommon
{
    /// <summary>
    ///     树形结构服务
    /// </summary>
    public class TreeAppService : AppServiceBase, ITreeAppService
    {
        private readonly IRepository<ColumnInfo, long> _columnInfoRepository;

        public TreeAppService(
            IRepository<ColumnInfo, long> columnInfoRepository
        )
        {
            _columnInfoRepository = columnInfoRepository;
        }

        /// <summary>
        ///     获取栏目 树级列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<TreeOutputDto> GetColumnInfoTreeNodes(GetTreeNodesInputDto input)
        {
            var data = _columnInfoRepository.GetAll().Where(p => p.ParentId == input.ParentId).ToList();
            var output = new TreeOutputDto
            {
                Data = data.Select(p => new TreeItemDto
                {
                    Data = new TreeItemDataDto
                    {
                        Title = p.Title,
                        Id = p.Id
                    }
                }).ToList()
            };

            foreach (var treeItemDto in output.Data)
            {
                treeItemDto.Children = _columnInfoRepository.GetAll().Where(p => p.ParentId == treeItemDto.Data.Id)
                    .ToList().Select(p => new TreeItemDto
                    {
                        Data = new TreeItemDataDto
                        {
                            Title = p.Title,
                            Id = p.Id
                        }
                    }).ToList();
                treeItemDto.Leaf = treeItemDto.Children == null || treeItemDto.Children.Count == 0;
            }

            return Task.FromResult(output);
        }
    }
}