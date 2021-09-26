// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : WebLogAppService.cs
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
using Abp.AspNetZeroCore.Net;
using Abp.Authorization;
using Magicodes.Admin.Application.Core;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Application.IO;
using Magicodes.Admin.Application.Logging.Dto;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Authorization;
using Magicodes.Admin.Core.Storage;

namespace Magicodes.Admin.Application.Logging
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Host_Maintenance)]
    public class WebLogAppService : AdminAppServiceBase, IWebLogAppService
    {
        private readonly IAppFolders _appFolders;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public WebLogAppService(IAppFolders appFolders, ITempFileCacheManager tempFileCacheManager)
        {
            _appFolders = appFolders;
            _tempFileCacheManager = tempFileCacheManager;
        }

        public GetLatestWebLogsOutput GetLatestWebLogs()
        {
            var directory = new DirectoryInfo(_appFolders.WebLogsFolder);
            if (!directory.Exists) return new GetLatestWebLogsOutput {LatestWebLogLines = new List<string>()};

            var lastLogFile = directory.GetFiles("*.*", SearchOption.AllDirectories)
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();

            if (lastLogFile == null) return new GetLatestWebLogsOutput();

            var lines = AppFileHelper.ReadLines(lastLogFile.FullName).Reverse().Take(1000).ToList();
            var logLineCount = 0;
            var lineCount = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("DEBUG") ||
                    line.StartsWith("INFO") ||
                    line.StartsWith("WARN") ||
                    line.StartsWith("ERROR") ||
                    line.StartsWith("FATAL"))
                    logLineCount++;

                lineCount++;

                if (logLineCount == 100) break;
            }

            return new GetLatestWebLogsOutput
            {
                LatestWebLogLines = lines.Take(lineCount).Reverse().ToList()
            };
        }

        public FileDto DownloadWebLogs()
        {
            //Create temporary copy of logs
            var logFiles = GetAllLogFiles();

            //Create the zip file
            var zipFileDto = new FileDto("WebSiteLogs.zip", MimeTypeNames.ApplicationZip);

            using (var outputZipFileStream = new MemoryStream())
            {
                using (var zipStream = new ZipArchive(outputZipFileStream, ZipArchiveMode.Create))
                {
                    foreach (var logFile in logFiles)
                    {
                        var entry = zipStream.CreateEntry(logFile.Name);
                        using (var entryStream = entry.Open())
                        {
                            using (var fs = new FileStream(logFile.FullName, FileMode.Open, FileAccess.Read,
                                FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
                            {
                                fs.CopyTo(entryStream);
                                entryStream.Flush();
                            }
                        }
                    }
                }

                _tempFileCacheManager.SetFile(zipFileDto.FileToken, outputZipFileStream.ToArray());
            }

            return zipFileDto;
        }

        private List<FileInfo> GetAllLogFiles()
        {
            var directory = new DirectoryInfo(_appFolders.WebLogsFolder);
            return directory.GetFiles("*.*", SearchOption.TopDirectoryOnly).ToList();
        }
    }
}