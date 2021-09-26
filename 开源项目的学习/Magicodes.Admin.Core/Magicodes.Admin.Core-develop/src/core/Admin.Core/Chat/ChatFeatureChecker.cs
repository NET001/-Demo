// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ChatFeatureChecker.cs
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

using Abp.Application.Features;
using Abp.UI;
using Magicodes.Admin.Core.Features;

namespace Magicodes.Admin.Core.Chat
{
    public class ChatFeatureChecker : AdminDomainServiceBase, IChatFeatureChecker
    {
        private readonly IFeatureChecker _featureChecker;

        public ChatFeatureChecker(
            IFeatureChecker featureChecker
        )
        {
            _featureChecker = featureChecker;
        }

        public void CheckChatFeatures(int? sourceTenantId, int? targetTenantId)
        {
            CheckChatFeaturesInternal(sourceTenantId, targetTenantId, ChatSide.Sender);
            CheckChatFeaturesInternal(targetTenantId, sourceTenantId, ChatSide.Receiver);
        }

        private void CheckChatFeaturesInternal(int? sourceTenantId, int? targetTenantId, ChatSide side)
        {
            var localizationPosfix = side == ChatSide.Sender ? "ForSender" : "ForReceiver";
            if (sourceTenantId.HasValue)
            {
                if (!_featureChecker.IsEnabled(sourceTenantId.Value, AppFeatures.ChatFeature))
                    throw new UserFriendlyException(L("ChatFeatureIsNotEnabled" + localizationPosfix));

                if (targetTenantId.HasValue)
                {
                    if (sourceTenantId == targetTenantId) return;

                    if (!_featureChecker.IsEnabled(sourceTenantId.Value, AppFeatures.TenantToTenantChatFeature))
                        throw new UserFriendlyException(
                            L("TenantToTenantChatFeatureIsNotEnabled" + localizationPosfix));
                }
                else
                {
                    if (!_featureChecker.IsEnabled(sourceTenantId.Value, AppFeatures.TenantToHostChatFeature))
                        throw new UserFriendlyException(L("TenantToHostChatFeatureIsNotEnabled" + localizationPosfix));
                }
            }
            else
            {
                if (targetTenantId.HasValue)
                    if (!_featureChecker.IsEnabled(targetTenantId.Value, AppFeatures.TenantToHostChatFeature))
                        throw new UserFriendlyException(L("TenantToHostChatFeatureIsNotEnabled" +
                                                          (side == ChatSide.Sender ? "ForReceiver" : "ForSender")));
            }
        }
    }
}