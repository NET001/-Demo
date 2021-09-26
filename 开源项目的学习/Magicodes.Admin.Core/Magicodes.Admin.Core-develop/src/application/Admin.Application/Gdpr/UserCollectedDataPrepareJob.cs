// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserCollectedDataPrepareJob.cs
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

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Abp;
using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Threading;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core.Localization;
using Magicodes.Admin.Core.Notifications;
using Magicodes.Admin.Core.Storage;

namespace Magicodes.Admin.Application.Gdpr
{
    public class UserCollectedDataPrepareJob : BackgroundJob<UserIdentifier>, ITransientDependency
    {
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ISettingManager _settingManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public UserCollectedDataPrepareJob(
            IBinaryObjectManager binaryObjectManager,
            ITempFileCacheManager tempFileCacheManager,
            IAppNotifier appNotifier,
            ISettingManager settingManager)
        {
            _binaryObjectManager = binaryObjectManager;
            _tempFileCacheManager = tempFileCacheManager;
            _appNotifier = appNotifier;
            _settingManager = settingManager;
        }

        [UnitOfWork]
        public override void Execute(UserIdentifier args)
        {
            using (UnitOfWorkManager.Current.SetTenantId(args.TenantId))
            {
                var userLanguage = AsyncHelper.RunSync(() =>
                    _settingManager.GetSettingValueForUserAsync(LocalizationSettingNames.DefaultLanguage, args.TenantId,
                        args.UserId));
                var culture = CultureHelper.GetCultureInfoByChecking(userLanguage);

                using (CultureInfoHelper.Use(culture))
                {
                    var files = new List<FileDto>();

                    using (var scope = IocManager.Instance.CreateScope())
                    {
                        var providers = scope.ResolveAll<IUserCollectedDataProvider>();
                        foreach (var provider in providers)
                        {
                            var providerFiles = AsyncHelper.RunSync(() => provider.GetFiles(args));
                            if (providerFiles.Any()) files.AddRange(providerFiles);
                        }
                    }

                    var zipFile = new BinaryObject
                    {
                        TenantId = args.TenantId,
                        Bytes = CompressFiles(files)
                    };

                    // Save zip file to object manager.
                    AsyncHelper.RunSync(() => _binaryObjectManager.SaveAsync(zipFile));

                    // Send notification to user.
                    AsyncHelper.RunSync(() => _appNotifier.GdprDataPrepared(args, zipFile.Id));
                }
            }
        }

        private byte[] CompressFiles(List<FileDto> files)
        {
            using (var outputZipFileStream = new MemoryStream())
            {
                using (var zipStream = new ZipArchive(outputZipFileStream, ZipArchiveMode.Create))
                {
                    foreach (var file in files)
                    {
                        var fileBytes = _tempFileCacheManager.GetFile(file.FileToken);
                        var entry = zipStream.CreateEntry(file.FileName);

                        using (var originalFileStream = new MemoryStream(fileBytes))
                        using (var zipEntryStream = entry.Open())
                        {
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }

                return outputZipFileStream.ToArray();
            }
        }
    }
}