// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppUserOpenIdManager.cs
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

using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;

namespace Magicodes.Admin.Core.Authorization.OpenId
{
    public class AppUserOpenIdManager : IAppUserOpenIdManager, ITransientDependency
    {
        private readonly IRepository<AppUserOpenId, long> _appUserOpenIdRepository;

        public AppUserOpenIdManager(IRepository<AppUserOpenId, long> appUserOpenIdRepository)
        {
            _appUserOpenIdRepository = appUserOpenIdRepository;
            AbpSession = NullAbpSession.Instance;
        }

        public IAbpSession AbpSession { get; set; }

        public async Task<string> GetOpenId(OpenIdPlatforms from)
        {
            var user = await _appUserOpenIdRepository.FirstOrDefaultAsync(p =>
                p.UserId == AbpSession.GetUserId() && p.From == from);
            return user?.OpenId;
        }
    }
}