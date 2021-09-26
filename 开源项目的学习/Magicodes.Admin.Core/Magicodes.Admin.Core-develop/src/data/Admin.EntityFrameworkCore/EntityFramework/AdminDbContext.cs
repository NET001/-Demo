// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminDbContext.cs
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

using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Magicodes.Admin.Core.Attachments;
using Magicodes.Admin.Core.Authorization.OpenId;
using Magicodes.Admin.Core.Authorization.Roles;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Chat;
using Magicodes.Admin.Core.Contents;
using Magicodes.Admin.Core.Editions;
using Magicodes.Admin.Core.Friendships;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.Core.MultiTenancy.Accounting;
using Magicodes.Admin.Core.MultiTenancy.Payments;
using Magicodes.Admin.Core.Storage;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.EntityFrameworkCore.EntityFramework
{
    public partial class AdminDbContext : AbpZeroDbContext<Tenant, Role, User, AdminDbContext>,
        IAbpPersistedGrantDbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            : base(options)
        {
        }
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<AttachmentInfo> AttachmentInfos { get; set; }

        public virtual DbSet<ObjectAttachmentInfo> ObjectAttachmentInfos { get; set; }

        public virtual DbSet<ArticleInfo> ArticleInfos { get; set; }

        public virtual DbSet<ArticleSourceInfo> ArticleSourceInfos { get; set; }

        public virtual DbSet<ArticleTagInfo> ArticleTagInfos { get; set; }

        public virtual DbSet<ColumnInfo> ColumnInfos { get; set; }

        public virtual DbSet<AppUserOpenId> AppUserOpenIds { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b => { b.HasIndex(e => new {e.TenantId}); });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new {e.TenantId, e.UserId, e.ReadState});
                b.HasIndex(e => new {e.TenantId, e.TargetUserId, e.ReadState});
                b.HasIndex(e => new {e.TargetTenantId, e.TargetUserId, e.ReadState});
                b.HasIndex(e => new {e.TargetTenantId, e.UserId, e.ReadState});
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new {e.TenantId, e.UserId});
                b.HasIndex(e => new {e.TenantId, e.FriendUserId});
                b.HasIndex(e => new {e.FriendTenantId, e.UserId});
                b.HasIndex(e => new {e.FriendTenantId, e.FriendUserId});
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new {e.SubscriptionEndDateUtc});
                b.HasIndex(e => new {e.CreationTime});
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new {e.Status, e.CreationTime});
                b.HasIndex(e => new {e.PaymentId, e.Gateway});
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}