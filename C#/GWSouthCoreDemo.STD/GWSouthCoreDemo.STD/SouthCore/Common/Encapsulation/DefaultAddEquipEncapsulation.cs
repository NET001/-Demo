using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SouthCore.Common
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public interface IDataManageService
    {
        /// <summary>
        /// 根据条件获取TEntity表信息
        /// </summary>
        /// <returns></returns>
        public List<T> GetList<T>(Func<T, bool> predicate) where T : class;
        /// <summary>
        /// 删除<T>表信息
        /// </summary>
        /// <returns></returns>
        public int Delete<T>(T obj);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DeleteList<T>(List<T> list) where T : class;
        /// <summary>
        /// 添加<T>表信息
        /// </summary>
        /// <returns></returns>
        public T Add<T>(T obj) where T : class;
        /// <summary>
        /// 批量添加<T>表信息
        /// </summary>
        /// <returns></returns>
        public int AddList<T>(List<T> equipList) where T : class;
    }
    /// <summary>
    /// 默认实现
    /// </summary>
    public abstract class DefaultAddEquipEncapsulation : BaseAddEquipEncapsulation
    {
        /// <summary>
        /// 设备线程数量
        /// </summary>
        public int EquipThreadQuantity { get; set; } = 50;

        IDataManageService dataManageService;
        public DefaultAddEquipEncapsulation(string dllName, string iotequip_nm, IDataManageService dataManageService) : base(dllName, iotequip_nm)
        {
            this.dataManageService = dataManageService;
        }
        #region 获取操作
        protected override IotEquipDto GetIotEquip()
        {
            return dataManageService.GetList<IotEquipDto>(t => t.communication_drv == dllName && t.equip_nm == iotequip_nm).FirstOrDefault();
        }
        protected override (List<IotYcpDto>, List<IotYxpDto>, List<IotSetParmDto>) GetIotEquipProperty(int equip_no)
        {
            List<IotYcpDto> iotYcpDtos = dataManageService.GetList<IotYcpDto>(t => t.equip_no == equip_no).ToList();
            List<IotYxpDto> iotYxpDto = dataManageService.GetList<IotYxpDto>(t => t.equip_no == equip_no).ToList();
            List<IotSetParmDto> iotSetParmDto = dataManageService.GetList<IotSetParmDto>(t => t.equip_no == equip_no);
            return (iotYcpDtos, iotYxpDto, iotSetParmDto);
        }
        protected override List<EquipDto> GetEquips(int equip_no)
        {
            return dataManageService.GetList<EquipDto>(t => t.raw_equip_no == equip_no);
        }
        protected override (List<YcpDto>, List<YxpDto>, List<SetParmDto>) GetEquipsProperty(List<int> equip_nos)
        {
            List<YcpDto> ycps = dataManageService.GetList<YcpDto>(t => equip_nos.Contains(t.equip_no));
            List<YxpDto> yxps = dataManageService.GetList<YxpDto>(t => equip_nos.Contains(t.equip_no));
            List<SetParmDto> setParms = dataManageService.GetList<SetParmDto>(t => equip_nos.Contains(t.equip_no));
            return (ycps, yxps, setParms);
        }
        protected override EGroupDto GetEGroup(string groupName)
        {
            EGroupDto eGroup = dataManageService.GetList<EGroupDto>(t => t.GroupName == groupName).FirstOrDefault();
            if (eGroup == null)
            {
                eGroup = new EGroupDto()
                {
                    GroupName = groupName,
                    ParentGroupId = 1
                };
                eGroup = dataManageService.Add(eGroup);
            }
            return eGroup;
        }
        protected override List<YcpDto> GetAddEquipYcps(EquipDto equip, IotEquipDto iotEquip, List<IotYcpDto> iotYcps)
        {
            List<YcpDto> result = iotYcps.Select(t => new YcpDto()
            {
                sta_n = t.sta_n,
                equip_no = equip.equip_no,
                yc_no = t.yc_no,
                yc_nm = t.yc_nm,
                mapping = t.mapping,
                yc_min = t.yc_min,
                yc_max = t.yc_max,
                physic_min = t.physic_min,
                physic_max = t.physic_max,
                val_min = t.val_min,
                restore_min = t.restore_min,
                restore_max = t.restore_max,
                val_max = t.val_max,
                val_trait = t.val_trait,
                main_instruction = t.main_instruction,
                minor_instruction = t.minor_instruction,
                safe_bgn = t.safe_bgn,
                safe_end = t.safe_end,
                alarm_acceptable_time = t.alarm_acceptable_time,
                restore_acceptable_time = t.restore_acceptable_time,
                alarm_repeat_time = t.alarm_repeat_time,
                proc_advice = t.proc_advice,
                lvl_level = t.lvl_level,
                outmin_evt = t.outmin_evt,
                outmax_evt = t.outmax_evt,
                wave_file = t.wave_file,
                related_pic = t.related_pic,
                alarm_scheme = t.alarm_scheme,
                curve_rcd = t.curve_rcd,
                curve_limit = t.curve_limit,
                alarm_shield = t.alarm_shield,
                unit = t.unit,
                AlarmRiseCycle = t.AlarmRiseCycle,
                Reserve1 = t.Reserve1,
                Reserve2 = t.Reserve2,
                Reserve3 = t.Reserve3,
                related_video = t.related_video,
                ZiChanID = t.ZiChanID,
                PlanNo = t.PlanNo,
                SafeTime = t.SafeTime,
                GWValue = "",
                GWTime = null,
                yc_code = t.yc_code,
            }).ToList();
            GetAddEquipYcpsAOP(equip, iotEquip, iotYcps, result);
            return result;
        }
        protected override List<YxpDto> GetAddEquipYxps(EquipDto equip, IotEquipDto iotEquip, List<IotYxpDto> iotYxps)
        {
            List<YxpDto> result = iotYxps.Select(t => new YxpDto()
            {
                sta_n = t.sta_n,
                equip_no = equip.equip_no,
                yx_no = t.yx_no,
                yx_nm = t.yx_nm,
                proc_advice_r = t.proc_advice_r,
                proc_advice_d = t.proc_advice_d,
                level_r = t.level_r,
                level_d = t.level_d,
                evt_01 = t.evt_01,
                evt_10 = t.evt_10,
                main_instruction = t.minor_instruction,
                minor_instruction = t.minor_instruction,
                safe_bgn = t.safe_bgn,
                safe_end = t.safe_end,
                alarm_acceptable_time = t.alarm_acceptable_time,
                restore_acceptable_time = t.restore_acceptable_time,
                alarm_repeat_time = t.alarm_repeat_time,
                wave_file = t.wave_file,
                related_pic = t.related_pic,
                alarm_scheme = t.alarm_scheme,
                inversion = t.inversion,
                initval = t.initval,
                val_trait = t.val_trait,
                alarm_shield = t.alarm_shield,
                AlarmRiseCycle = t.AlarmRiseCycle,
                related_video = t.related_video,
                ZiChanID = t.ZiChanID,
                PlanNo = t.PlanNo,
                SafeTime = t.SafeTime,
                curve_rcd = t.curve_rcd,
                Reserve1 = t.Reserve1,
                Reserve2 = t.Reserve2,
                Reserve3 = t.Reserve3,
                GWValue = "",
                GWTime = null,
                yx_code = t.yx_code,
            }).ToList();
            GetAddEquipYxpsAOP(equip, iotEquip, iotYxps, result);
            return result;
        }
        protected override List<SetParmDto> GetAddEquipSetParms(EquipDto equip, IotEquipDto iotEquips, List<IotSetParmDto> iotSetParms)
        {
            List<SetParmDto> result = iotSetParms.Select(t => new SetParmDto()
            {
                sta_n = t.sta_n,
                equip_no = equip.equip_no,
                main_instruction = t.main_instruction,
                minor_instruction = t.main_instruction,
                Reserve2 = t.Reserve2,
                Reserve1 = t.Reserve1,
                qr_equip_no = t.qr_equip_no,
                EnableVoice = t.EnableVoice,
                VoiceKeys = t.VoiceKeys,
                canexecution = t.canexecution,
                value = t.value,
                Reserve3 = t.Reserve3,
                action = t.action,
                set_type = t.set_type,
                set_nm = t.set_nm,
                set_no = t.set_no,
                record = t.record,
                set_code = t.set_code,
            }).ToList();
            GetAddEquipSetParmsAOP(equip, iotEquips, iotSetParms, result);
            return result;
        }
        #endregion
        #region 更新操作
        protected override EquipDto AddEquip(int i, object obj, IotEquipDto iotEquip)
        {
            //默认为50
            string local_addr = dllName + "_" + i / EquipThreadQuantity;
            EquipDto equip = new EquipDto()
            {
                sta_n = iotEquip.sta_n,
                equip_nm = iotEquip.equip_nm,
                equip_detail = iotEquip.equip_nm,
                acc_cyc = iotEquip.acc_cyc,
                related_pic = iotEquip.related_pic,
                proc_advice = iotEquip.proc_advice,
                out_of_contact = iotEquip.out_of_contact,
                contacted = iotEquip.contacted,
                event_wav = iotEquip.event_wav,
                communication_drv = iotEquip.communication_drv,
                local_addr = local_addr,
                equip_addr = iotEquip.equip_addr,
                communication_param = iotEquip.communication_param,
                communication_time_param = iotEquip.communication_time_param,
                raw_equip_no = iotEquip.equip_no,
                tabname = iotEquip.tabname,
                alarm_scheme = iotEquip.alarm_scheme,
                attrib = iotEquip.attrib,
                sta_IP = iotEquip.sta_IP,
                AlarmRiseCycle = iotEquip.AlarmRiseCycle,
                Reserve1 = iotEquip.Reserve1,
                Reserve2 = iotEquip.Reserve2,
                Reserve3 = iotEquip.Reserve3,
                related_video = iotEquip.related_video,
                ZiChanID = iotEquip.ZiChanID,
                PlanNo = iotEquip.PlanNo,
                SafeTime = iotEquip.SafeTime,
                backup = iotEquip.backup,
            };
            AddEquipAOP(obj, iotEquip, equip);
            equip = dataManageService.Add(equip);
            return equip;
        }
        /// <summary>
        /// 默认不实现删除数据的操作
        /// </summary>
        /// <param name="equips"></param>
        /// <returns></returns>
        protected override List<EquipDto> DeleteData(List<EquipDto> equips)
        {
            return null;
        }
        protected override void DeleteEquips(List<EquipDto> equips)
        {
            dataManageService.DeleteList(equips);
        }
        protected override void AddGroupLists(List<EGroupListDto> eGroupLists)
        {
            dataManageService.AddList(eGroupLists);
        }
        protected override void AddYcps(List<YcpDto> list)
        {
            dataManageService.AddList(list);
        }
        protected override void AddYxps(List<YxpDto> list)
        {
            dataManageService.AddList(list);
        }
        protected override void AddSetParms(List<SetParmDto> list)
        {
            dataManageService.AddList(list);
        }
        protected override void DeleteGuidance(List<EquipDto> equips)
        {
            var guidanceData = equips.Where(t => t.equip_nm == "引导设备");
            if (guidanceData.Count() != 0)
            {
                dataManageService.Delete(guidanceData.First());
            }
        }
        #endregion
        #region Default的扩展
        protected virtual void AddEquipAOP(object obj, IotEquipDto iotEquip, EquipDto equipDto)
        {

        }
        protected virtual void GetAddEquipYcpsAOP(EquipDto equip, IotEquipDto iotEquip, List<IotYcpDto> iotYcps, List<YcpDto> result)
        {

        }
        protected virtual void GetAddEquipYxpsAOP(EquipDto equip, IotEquipDto iotEquip, List<IotYxpDto> iotYxps, List<YxpDto> result)
        {

        }
        protected virtual void GetAddEquipSetParmsAOP(EquipDto equip, IotEquipDto iotEquips, List<IotSetParmDto> iotSetParms, List<SetParmDto> result)
        {

        }
        #endregion
    }
}

