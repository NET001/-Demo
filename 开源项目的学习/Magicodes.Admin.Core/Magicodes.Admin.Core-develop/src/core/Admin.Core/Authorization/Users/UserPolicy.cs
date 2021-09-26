// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserPolicy.cs
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
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Magicodes.Admin.Core.Features;

namespace Magicodes.Admin.Core.Authorization.Users
{
    public class UserPolicy : AdminServiceBase, IUserPolicy
    {
        private readonly IFeatureChecker _featureChecker;
        private readonly IRepository<User, long> _userRepository;

        public UserPolicy(IFeatureChecker featureChecker, IRepository<User, long> userRepository)
        {
            _featureChecker = featureChecker;
            _userRepository = userRepository;
        }

        public async Task CheckMaxUserCountAsync(int tenantId)
        {
            var maxUserCount = (await _featureChecker.GetValueAsync(tenantId, AppFeatures.MaxUserCount)).To<int>();
            if (maxUserCount <= 0) return;

            var currentUserCount = await _userRepository.CountAsync();
            if (currentUserCount >= maxUserCount)
                throw new UserFriendlyException(L("MaximumUserCount_Error_Message"),
                    L("MaximumUserCount_Error_Detail", maxUserCount));
        }
    }
}