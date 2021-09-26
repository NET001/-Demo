// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TenantDemoDataBuilder.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Magicodes.Admin.Core.Authorization.Roles;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Chat;
using Magicodes.Admin.Core.Configuration;
using Magicodes.Admin.Core.Friendships;
using Magicodes.Admin.Core.Storage;
using Microsoft.Extensions.Configuration;

namespace Magicodes.Admin.Core.MultiTenancy.Demo
{
    /// <summary>
    ///     Used to build demo data for new tenants.
    ///     Creates sample organization units, users... etc.
    ///     It works only if in DEMO mode ("App.DemoMode" should be "true" in web.config). Otherwise, does nothing.
    /// </summary>
    public class TenantDemoDataBuilder : AdminServiceBase, ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IAppFolders _appFolders;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<ChatMessage, long> _chatMessageRepository;
        private readonly IFriendshipManager _friendshipManager;

        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly RandomUserGenerator _randomUserGenerator;
        private readonly UserManager _userManager;

        public TenantDemoDataBuilder(
            OrganizationUnitManager organizationUnitManager,
            UserManager userManager,
            RandomUserGenerator randomUserGenerator,
            IBinaryObjectManager binaryObjectManager,
            IAppFolders appFolders,
            IFriendshipManager friendshipManager,
            IRepository<ChatMessage, long> chatMessageRepository,
            IAppConfigurationAccessor configurationAccessor)
        {
            _organizationUnitManager = organizationUnitManager;
            _userManager = userManager;
            _randomUserGenerator = randomUserGenerator;
            _binaryObjectManager = binaryObjectManager;
            _appFolders = appFolders;
            _friendshipManager = friendshipManager;
            _chatMessageRepository = chatMessageRepository;

            _appConfiguration = configurationAccessor.Configuration;
        }

        public bool IsInDemoMode =>
            string.Equals(_appConfiguration["App:DemoMode"], "true", StringComparison.OrdinalIgnoreCase);

        public async Task BuildForAsync(Tenant tenant)
        {
            if (!IsInDemoMode) return;

            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                await BuildForInternalAsync(tenant);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            await EnableIsNewRegisteredUserActiveByDefaultAsync(tenant);
        }

        private async Task BuildForInternalAsync(Tenant tenant)
        {
            //Create Organization Units

            var organizationUnits = new List<OrganizationUnit>();

            var producing = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Producing");

            var researchAndDevelopment =
                await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Research & Development", producing);

            var ivrProducts = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "IVR Related Products",
                researchAndDevelopment);
            var voiceTech = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Voice Technologies",
                researchAndDevelopment);
            var inhouseProjects = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Inhouse Projects",
                researchAndDevelopment);

            var qualityManagement =
                await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Quality Management", producing);
            var testing = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Testing", producing);

            var selling = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Selling");

            var marketing = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Marketing", selling);
            var sales = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Sales", selling);
            var custRelations =
                await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Customer Relations", selling);

            var supporting = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Supporting");

            var buying = await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Buying", supporting);
            var humanResources =
                await CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Human Resources", supporting);

            //Create users

            var users = _randomUserGenerator.GetRandomUsers(RandomHelper.GetRandom(12, 26), tenant.Id);
            foreach (var user in users)
            {
                //Create the user
                await _userManager.CreateAsync(user);
                await CurrentUnitOfWork.SaveChangesAsync();

                //Add to roles
                await _userManager.AddToRoleAsync(user, StaticRoleNames.Tenants.User);

                //Add to OUs
                var randomOus = RandomHelper.GenerateRandomizedList(organizationUnits)
                    .Take(RandomHelper.GetRandom(0, 3));
                foreach (var ou in randomOus) await _userManager.AddToOrganizationUnitAsync(user, ou);

                //Set profile picture
                if (RandomHelper.GetRandom(100) < 70) //A user will have a profile picture in 70% probability.
                    await SetRandomProfilePictureAsync(user);
            }

            //Set a picture to admin!
            var admin = await _userManager.FindByNameAsync(AbpUserBase.AdminUserName);
            await SetRandomProfilePictureAsync(admin);

            //Create Friendships
            var friends = RandomHelper.GenerateRandomizedList(users).Take(3).ToList();
            foreach (var friend in friends)
            {
                await _friendshipManager.CreateFriendshipAsync(
                    new Friendship(
                        admin.ToUserIdentifier(),
                        friend.ToUserIdentifier(),
                        tenant.TenancyName,
                        friend.UserName,
                        friend.ProfilePictureId,
                        FriendshipState.Accepted)
                );

                await _friendshipManager.CreateFriendshipAsync(
                    new Friendship(
                        friend.ToUserIdentifier(),
                        admin.ToUserIdentifier(),
                        tenant.TenancyName,
                        admin.UserName,
                        admin.ProfilePictureId,
                        FriendshipState.Accepted)
                );
            }

            //Create chat message
            var friendWithMessage = RandomHelper.GenerateRandomizedList(friends).First();
            var sharedMessageId = Guid.NewGuid();
            _chatMessageRepository.InsertAndGetId(
                new ChatMessage(
                    friendWithMessage.ToUserIdentifier(),
                    admin.ToUserIdentifier(),
                    ChatSide.Sender,
                    L("Demo_SampleChatMessage"),
                    ChatMessageReadState.Read,
                    sharedMessageId,
                    ChatMessageReadState.Unread
                )
            );

            _chatMessageRepository.InsertAndGetId(
                new ChatMessage(
                    admin.ToUserIdentifier(),
                    friendWithMessage.ToUserIdentifier(),
                    ChatSide.Receiver,
                    L("Demo_SampleChatMessage"),
                    ChatMessageReadState.Unread,
                    sharedMessageId,
                    ChatMessageReadState.Read
                )
            );
        }

        private async Task EnableIsNewRegisteredUserActiveByDefaultAsync(Tenant tenant)
        {
            await SettingManager.ChangeSettingForTenantAsync(tenant.Id,
                AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, "true");
        }

        private async Task<OrganizationUnit> CreateAndSaveOrganizationUnit(List<OrganizationUnit> organizationUnits,
            Tenant tenant, string displayName, OrganizationUnit parent = null)
        {
            var organizationUnit =
                new OrganizationUnit(tenant.Id, displayName, parent == null ? (long?) null : parent.Id);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            organizationUnits.Add(organizationUnit);

            return organizationUnit;
        }

        private async Task SetRandomProfilePictureAsync(User user)
        {
            try
            {
                //Save a random profile picture
                var storedFile = new BinaryObject(user.TenantId, GetRandomProfilePictureBytes());
                await _binaryObjectManager.SaveAsync(storedFile);

                //Update new picture on the user
                user.ProfilePictureId = storedFile.Id;
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                //we can ignore this exception                
            }
        }

        private byte[] GetRandomProfilePictureBytes()
        {
            var fileName = string.Format("sample-profile-{0}.jpg", RandomHelper.GetRandom(1, 11).ToString("00"));
            var fullPath = Path.Combine(_appFolders.SampleProfileImagesFolder, fileName);

            if (!File.Exists(fullPath)) throw new Exception("Could not find sample profile picture on " + fullPath);

            return File.ReadAllBytes(fullPath);
        }
    }
}