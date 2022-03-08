using GWBasics.STD.Services;
using GWDataCenter;
using GWDataCenter.Database;
using GWSouthCoreDemo.STD.Model;
using SouthCore.Common;
using SouthCore.Default.GetData;
using SouthCore.Default.GetYc;
using SouthCore.Default.AddEquips;
using System;

namespace GWSouthCoreDemo.STD
{
    public class CEquip : CEquipBase
    {
        private string equipType;
        private object[] electricModel;
        private object[] waterModel;
        public override bool init(EquipItem item)
        {
            if (!base.init(item))
            {
                LogHelp.WriteLogFile("base.init=false");
                return false;
            }
            string[] time_params = item.communication_time_param.TryDefaultSplit('/', 3);
            string[] config_params = item.Reserve1.TryDefaultSplit('/', 2);
            if (!string.IsNullOrWhiteSpace(time_params[0]) && int.TryParse(time_params[0], out _))
            {
                AppServices.GetDataSleep = int.Parse(time_params[0]);
            }
            if (!string.IsNullOrWhiteSpace(time_params[1]) && int.TryParse(time_params[1], out _))
            {
                AppServices.SourceSyncSleep = int.Parse(time_params[1]);
            }
            if (!string.IsNullOrWhiteSpace(time_params[2]) && int.TryParse(time_params[2], out _))
            {
                AppServices.DataCacheSyncSleep = int.Parse(time_params[2]);
            }
            if (!AppServices.FirstInitFlag)
            {
                AppServices.FirstInit();
            }
            return true;
        }
        public override CommunicationState GetData(CEquipBase pEquip)
        {
            try
            {
                string[] id = pEquip.equipitem.communication_param.TryDefaultSplit('/', 2);
                equipType = id[0];
                if (id[0] == GlobalModel.EquipGroupNames[0])
                {
                    electricModel = AppServices.SouthHost.GetData(GlobalModel.EquipGroupNames[0], id[1]);
                }
                else if (id[0] == GlobalModel.EquipGroupNames[1])
                {
                    waterModel = AppServices.SouthHost.GetData(GlobalModel.EquipGroupNames[0], id[1]);
                }
                Sleep(AppServices.GetDataSleep);
                var result = base.GetData(pEquip);
                return result;
            }
            catch (Exception ex)
            {
                return CommunicationState.fail;
            }
        }
        public override bool GetYC(YcpTableRow r)
        {
            try
            {
                string result = "无";
                if (equipType == GlobalModel.EquipGroupNames[0])
                {
                    result = AppServices.SouthHost.GetYc(equipType, electricModel, r.main_instruction);
                }
                else if (equipType == GlobalModel.EquipGroupNames[1])
                {
                    result = AppServices.SouthHost.GetYc(equipType, waterModel, r.main_instruction);
                }
                SetYCData(r, result);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public override bool GetYX(YxpTableRow r)
        {
            try
            {
                SetYXData(r, "");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public override bool SetParm(string MainInstruct, string MinorInstruct, string Value)
        {
            switch (MainInstruct)
            {
                case "AddEquip":
                    {
                        AppServices.SouthHost.Services.AddEquipsDefault();
                    }
                    break;
            }
            return true;
        }
    }
}
