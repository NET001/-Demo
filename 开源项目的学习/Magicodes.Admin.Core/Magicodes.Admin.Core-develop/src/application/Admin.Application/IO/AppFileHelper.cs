// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppFileHelper.cs
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
using System.Linq;
using System.Text;
using Abp.IO;

namespace Magicodes.Admin.Application.IO
{
    public static class AppFileHelper
    {
        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000,
                FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null) yield return line;
            }
        }

        public static void DeleteFilesInFolderIfExists(string folderPath, string fileNameWithoutExtension)
        {
            var directory = new DirectoryInfo(folderPath);
            var tempUserProfileImages = directory.GetFiles(fileNameWithoutExtension + ".*", SearchOption.AllDirectories)
                .ToList();
            foreach (var tempUserProfileImage in tempUserProfileImages)
                FileHelper.DeleteIfExists(tempUserProfileImage.FullName);
        }
    }
}