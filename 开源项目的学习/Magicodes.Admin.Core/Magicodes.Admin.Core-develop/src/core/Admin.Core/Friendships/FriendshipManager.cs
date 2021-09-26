// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : FriendshipManager.cs
//           description :
// 
//           created by 雪雁 at  2019-06-17 10:17
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;

namespace Magicodes.Admin.Core.Friendships
{
    public class FriendshipManager : AdminDomainServiceBase, IFriendshipManager
    {
        private readonly IRepository<Friendship, long> _friendshipRepository;

        public FriendshipManager(IRepository<Friendship, long> friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }

        [UnitOfWork]
        public async Task CreateFriendshipAsync(Friendship friendship)
        {
            if (friendship.TenantId == friendship.FriendTenantId &&
                friendship.UserId == friendship.FriendUserId)
                throw new UserFriendlyException(L("YouCannotBeFriendWithYourself"));

            using (CurrentUnitOfWork.SetTenantId(friendship.TenantId))
            {
                _friendshipRepository.Insert(friendship);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        [UnitOfWork]
        public async Task UpdateFriendshipAsync(Friendship friendship)
        {
            using (CurrentUnitOfWork.SetTenantId(friendship.TenantId))
            {
                _friendshipRepository.Update(friendship);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        [UnitOfWork]
        public async Task<Friendship> GetFriendshipOrNullAsync(UserIdentifier user, UserIdentifier probableFriend)
        {
            using (CurrentUnitOfWork.SetTenantId(user.TenantId))
            {
                return await _friendshipRepository.FirstOrDefaultAsync(friendship =>
                    friendship.UserId == user.UserId &&
                    friendship.TenantId == user.TenantId &&
                    friendship.FriendUserId == probableFriend.UserId &&
                    friendship.FriendTenantId == probableFriend.TenantId);
            }
        }

        [UnitOfWork]
        public async Task BanFriendAsync(UserIdentifier userIdentifier, UserIdentifier probableFriend)
        {
            var friendship = await GetFriendshipOrNullAsync(userIdentifier, probableFriend);
            if (friendship == null)
                throw new Exception("Friendship does not exist between " + userIdentifier + " and " + probableFriend);

            friendship.State = FriendshipState.Blocked;
            await UpdateFriendshipAsync(friendship);
        }

        [UnitOfWork]
        public async Task AcceptFriendshipRequestAsync(UserIdentifier userIdentifier, UserIdentifier probableFriend)
        {
            var friendship = await GetFriendshipOrNullAsync(userIdentifier, probableFriend);
            if (friendship == null)
                throw new Exception("Friendship does not exist between " + userIdentifier + " and " + probableFriend);

            friendship.State = FriendshipState.Accepted;
            await UpdateFriendshipAsync(friendship);
        }
    }
}