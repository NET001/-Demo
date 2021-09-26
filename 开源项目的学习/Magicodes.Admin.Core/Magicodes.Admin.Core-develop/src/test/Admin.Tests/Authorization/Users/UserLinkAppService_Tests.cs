// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserLinkAppService_Tests.cs
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
using Magicodes.Admin.Application.Authorization.Users;
using Magicodes.Admin.Application.Authorization.Users.Dto;
using Shouldly;
using Xunit;

namespace Magicodes.Admin.Tests.Authorization.Users
{
    public class UserLinkAppService_Tests : UserAppServiceTestBase
    {
        public UserLinkAppService_Tests()
        {
            _userLinkAppService = Resolve<UserLinkAppService>();
        }

        private readonly IUserLinkAppService _userLinkAppService;

        [Fact]
        public async Task GetLinkedUsers()
        {
            CreateTestUsers();

            var user = await GetUserByUserNameAsync("jnash");

            AbpSession.UserId = user.Id;

            var linkedUsers = await _userLinkAppService.GetLinkedUsers(
                new GetLinkedUsersInput
                {
                    MaxResultCount = 10,
                    SkipCount = 0
                }
            );

            linkedUsers.Items.Count.ShouldBe(0);
        }

        [Fact]
        public async Task GetRecentlyUsedLinkedUsers()
        {
            CreateTestUsers();

            var user = await GetUserByUserNameAsync("jnash");

            AbpSession.UserId = user.Id;

            var linkedUsers = await _userLinkAppService.GetRecentlyUsedLinkedUsers();

            linkedUsers.Items.Count.ShouldBe(0);
        }

        [Fact]
        public async Task LinkToUser()
        {
            CreateTestUsers();

            var user = await GetUserByUserNameAsync("jnash");

            AbpSession.UserId = user.Id;

            await _userLinkAppService.LinkToUser(
                new LinkToUserInput
                {
                    Password = "123qwe",
                    TenancyName = "Default",
                    UsernameOrEmailAddress = "adams_d@gmail.com"
                }
            );

            var linkedUsers = await _userLinkAppService.GetLinkedUsers(
                new GetLinkedUsersInput
                {
                    MaxResultCount = 10,
                    SkipCount = 0
                }
            );

            linkedUsers.Items.Count.ShouldBe(1);
        }
    }
}