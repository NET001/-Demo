using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Timing;
using Magicodes.Admin.Core.Attachments;
using Magicodes.Admin.Unity.Storage.Default;

namespace Magicodes.Admin.Unity.Editor
{
    /// <summary>
    /// 编辑器辅助类
    /// </summary>
    public class EditorHelper : ISingletonDependency
    {
        public IStorageManager StorageManager { get; set; }
        private readonly IRepository<AttachmentInfo, long> _attachmentInfoRepository;

        public EditorHelper(IRepository<AttachmentInfo, long> attachmentInfoRepository)
        {
            this._attachmentInfoRepository = attachmentInfoRepository;
        }

        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// 将内容中的图片转换为图片存储
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> ConvertBase64ImagesForContent(string content)
        {
            // 定义正则表达式用来匹配 img 标签   
            var reg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            var matches = reg.Matches(content);
            var i = 0;
            var sUrlList = new string[matches.Count];


            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            //将Base64String转为图片并保存
            foreach (var item in sUrlList)
            {
                if (!item.Contains("base64,")) continue;
                var base64ArrStrings = item.Split("base64,");
                var base64String = Convert.FromBase64String(base64ArrStrings[1]);
                var type = base64ArrStrings[0];
                var ext = ".png";
                if (type.Contains("image/gif"))
                {
                    ext = ".gif";
                }
                else if (type.Contains("image/jpeg"))
                {
                    ext = ".jpg";
                }

                var stream = new MemoryStream(base64String);
                var tempFileName = Guid.NewGuid().ToString("N") + ext;
                await StorageManager.StorageProvider.SaveBlobStream((AbpSession.TenantId ?? 0).ToString(), tempFileName, stream);
                var blobInfo = await StorageManager.StorageProvider.GetBlobFileInfo((AbpSession.TenantId ?? 0).ToString(), tempFileName);
                var attach = new AttachmentInfo()
                {
                    ContentType = blobInfo.ContentType,
                    CreationTime = Clock.Now,
                    CreatorUserId = AbpSession.UserId,
                    FileLength = blobInfo.Length,
                    Name = "Editor_Temp",
                    TenantId = AbpSession.TenantId,
                    Url = blobInfo.Url,
                    BlobName = blobInfo.Name,
                    ContainerName = blobInfo.Container,
                    AttachmentType = AttachmentTypes.Image,
                    ContentMD5 = blobInfo.ContentMD5
                };
                _attachmentInfoRepository.Insert(attach);
                content = content.Replace(item, blobInfo.Url);
            }

            return content;
        }
    }
}
