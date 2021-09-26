// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : NamespaceStripper.cs
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

using System.Linq;
using Abp.Dependency;
using Abp.Extensions;

namespace Magicodes.Admin.Application.Auditing
{
    public class NamespaceStripper : INamespaceStripper, ITransientDependency
    {
        public string StripNameSpace(string serviceName)
        {
            if (serviceName.IsNullOrEmpty() || !serviceName.Contains(".")) return serviceName;

            if (serviceName.Contains("[")) return StripGenericNamespace(serviceName);

            return GetTextAfterLastDot(serviceName);
        }

        private static string GetTextAfterLastDot(string text)
        {
            var lastDotIndex = text.LastIndexOf('.');
            return text.Substring(lastDotIndex + 1);
        }

        private static string StripGenericNamespace(string serviceName)
        {
            var serviceNameParts = serviceName.Split('[').Where(s => !s.IsNullOrEmpty()).ToList();
            var genericServiceName = "";
            var openBracketCount = 0;

            for (var i = 0; i < serviceNameParts.Count; i++)
            {
                var serviceNamePart = serviceNameParts[i];
                if (serviceNamePart.Contains("`"))
                {
                    genericServiceName +=
                        GetTextAfterLastDot(serviceNamePart.Substring(0, serviceNamePart.IndexOf('`'))) + "<";
                    openBracketCount++;
                }

                if (serviceNamePart.Contains(","))
                {
                    genericServiceName +=
                        GetTextAfterLastDot(serviceNamePart.Substring(0, serviceNamePart.IndexOf(',')));
                    if (i + 1 < serviceNameParts.Count && serviceNameParts[i + 1].Contains(","))
                    {
                        genericServiceName += ", ";
                    }
                    else
                    {
                        genericServiceName += ">";
                        openBracketCount--;
                    }
                }
            }

            for (var i = 0; i < openBracketCount; i++) genericServiceName += ">";

            return genericServiceName;
        }
    }
}