// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ChatMessageListExcelExporter.cs
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
using System.Linq;
using Magicodes.Admin.Application.Chat.Dto;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Application.DataExporting.Excel.EpPlus;
using Magicodes.Admin.Core.Chat;
using Magicodes.Admin.Core.Storage;

namespace Magicodes.Admin.Application.Chat.Exporting
{
    public class ChatMessageListExcelExporter : EpPlusExcelExporterBase, IChatMessageListExcelExporter
    {
        public ChatMessageListExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ChatMessageExportDto> messages)
        {
            var tenancyName = messages.Count > 0 ? messages.First().TargetTenantName : L("Anonymous");
            var userName = messages.Count > 0 ? messages.First().TargetUserName : L("Anonymous");

            return CreateExcelPackage(
                $"Chat_{tenancyName}_{userName}.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Messages"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ChatMessage_From"),
                        L("ChatMessage_To"),
                        L("Message"),
                        L("ReadState"),
                        L("CreationTime")
                    );

                    AddObjects(
                        sheet, 2, messages,
                        _ => _.Side == ChatSide.Receiver ? _.TargetTenantName + "/" + _.TargetUserName : L("You"),
                        _ => _.Side == ChatSide.Receiver ? L("You") : _.TargetTenantName + "/" + _.TargetUserName,
                        _ => _.Message,
                        _ => _.Side == ChatSide.Receiver ? _.ReadState : _.ReceiverReadState,
                        _ => _.CreationTime
                    );

                    //Formatting cells
                    var timeColumn = sheet.Column(5);
                    timeColumn.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                });
        }
    }
}