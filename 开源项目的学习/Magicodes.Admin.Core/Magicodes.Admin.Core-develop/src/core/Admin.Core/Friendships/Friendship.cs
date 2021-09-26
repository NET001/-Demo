// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : Friendship.cs
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Magicodes.Admin.Core.Friendships
{
    [Table("AppFriendships")]
    public class Friendship : Entity<long>, IHasCreationTime, IMayHaveTenant
    {
        public Friendship(UserIdentifier user, UserIdentifier probableFriend, string probableFriendTenancyName,
            string probableFriendUserName, Guid? probableFriendProfilePictureId, FriendshipState state)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (probableFriend == null) throw new ArgumentNullException(nameof(probableFriend));

            if (!Enum.IsDefined(typeof(FriendshipState), state))
                throw new Exception("Invalid FriendshipState value: " + state);

            UserId = user.UserId;
            TenantId = user.TenantId;
            FriendUserId = probableFriend.UserId;
            FriendTenantId = probableFriend.TenantId;
            FriendTenancyName = probableFriendTenancyName;
            FriendUserName = probableFriendUserName;
            State = state;
            FriendProfilePictureId = probableFriendProfilePictureId;

            CreationTime = Clock.Now;
        }

        protected Friendship()
        {
        }

        public long UserId { get; set; }

        public long FriendUserId { get; set; }

        public int? FriendTenantId { get; set; }

        [Required]
        [MaxLength(AbpUserBase.MaxUserNameLength)]
        public string FriendUserName { get; set; }

        public string FriendTenancyName { get; set; }

        public Guid? FriendProfilePictureId { get; set; }

        public FriendshipState State { get; set; }

        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }
    }
}