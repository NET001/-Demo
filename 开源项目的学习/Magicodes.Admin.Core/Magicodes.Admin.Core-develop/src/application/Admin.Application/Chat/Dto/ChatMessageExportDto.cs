// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ChatMessageExportDto.cs
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

using System;
using Magicodes.Admin.Core.Chat;

namespace Magicodes.Admin.Application.Chat.Dto
{
    public class ChatMessageExportDto
    {
        public long TargetUserId { get; set; }

        public string TargetUserName { get; set; }

        public string TargetTenantName { get; set; }

        public int? TargetTenantId { get; set; }

        public ChatSide Side { get; set; }

        public ChatMessageReadState ReadState { get; set; }

        public ChatMessageReadState ReceiverReadState { get; set; }

        public string Message { get; set; }

        public DateTime CreationTime { get; set; }
    }
}