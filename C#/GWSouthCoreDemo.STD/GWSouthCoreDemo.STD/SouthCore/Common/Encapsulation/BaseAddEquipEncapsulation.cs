using System;
using GWDataCenter;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace SouthCore.Common
{
    public abstract class BaseAddEquipEncapsulation
    {
        protected string iotequip_nm;
        protected string dllName;
        public BaseAddEquipEncapsulation(string dllName, string iotequip_nm)
        {
            this.iotequip_nm = iotequip_nm;
            this.dllName = dllName;
        }
        /// <summary>
        /// 添加设备
        /// </summary>
        public void AddEquips()
        {
            //关于动态设备变更
            bool equipUpdateFlag = false;
            bool equipAddFlag = false;
            List<ChangedEquip> editEquips = new List<ChangedEquip>();
            List<ChangedEquip> addEquips = new List<ChangedEquip>();

            IotEquipDto iotEquip = GetIotEquip();
            //设备分组表
            List<EGroupListDto> eGroupLists = new List<EGroupListDto>();
            if (iotEquip != null)
            {
                (List<IotYcpDto>, List<IotYxpDto>, List<IotSetParmDto>) iotEquipProperty = GetIotEquipProperty(iotEquip.equip_no);
                List<IotYcpDto> iotYcps = iotEquipProperty.Item1;
                List<IotYxpDto> iotYxps = iotEquipProperty.Item2;
                List<IotSetParmDto> iotSetParms = iotEquipProperty.Item3;

                List<EquipDto> equips = GetEquips(iotEquip.equip_no);

                //补全设备属性
                if (equips.Count > 0)
                {
                    (List<YcpDto>, List<YxpDto>, List<SetParmDto>) equipsProperty = GetEquipsProperty(equips.Select(t => t.equip_no).ToList());

                    List<YcpDto> ycps = equipsProperty.Item1;
                    List<YxpDto> yxps = equipsProperty.Item2;
                    List<SetParmDto> setParms = equipsProperty.Item3;

                    List<YcpDto> deficiencyYcps = new List<YcpDto>();
                    List<YxpDto> deficiencyYxps = new List<YxpDto>();
                    List<SetParmDto> deficiencySetParms = new List<SetParmDto>();

                    //存储属性
                    foreach (var item in equips)
                    {
                        List<IotYcpDto> deficiencyIotYcps = iotYcps.Where(t => !ycps.Where(t => t.equip_no == item.equip_no).Select(t => t.yc_no).Contains(t.yc_no)).ToList();
                        deficiencyYcps.AddRange(GetAddEquipYcps(item, iotEquip, deficiencyIotYcps));
                        List<IotYxpDto> deficiencyIotYxps = iotYxps.Where(t => !yxps.Where(t => t.equip_no == item.equip_no).Select(t => t.yx_no).Contains(t.yx_no)).ToList();
                        deficiencyYxps.AddRange(GetAddEquipYxps(item, iotEquip, deficiencyIotYxps));
                        List<IotSetParmDto> deficiencyIotSetParms = iotSetParms.Where(t => !setParms.Where(t => t.equip_no == item.equip_no).Select(t => t.set_no).Contains(t.set_no)).ToList();
                        deficiencySetParms.AddRange(GetAddEquipSetParms(item, iotEquip, deficiencyIotSetParms));
                    }
                    //添加
                    if (deficiencyYcps.Count > 0)
                    {
                        equipUpdateFlag = true;
                        editEquips.AddRange(deficiencyYcps.GroupBy(t => t.equip_no).Select(t => new ChangedEquip()
                        {
                            iEqpNo = t.Key,
                            iStaNo = t.First().sta_n,
                            State = ChangedEquipState.Edit
                        }));
                        AddYcps(deficiencyYcps);
                    }
                    if (deficiencyYxps.Count > 0)
                    {
                        equipUpdateFlag = true;
                        editEquips.AddRange(deficiencyYxps.GroupBy(t => t.equip_no).Select(t => new ChangedEquip()
                        {
                            iEqpNo = t.Key,
                            iStaNo = t.First().sta_n,
                            State = ChangedEquipState.Edit
                        }));
                        AddYxps(deficiencyYxps);
                    }
                    if (deficiencySetParms.Count > 0)
                    {
                        equipUpdateFlag = true;
                        editEquips.AddRange(deficiencyYxps.GroupBy(t => t.equip_no).Select(t => new ChangedEquip()
                        {
                            iEqpNo = t.Key,
                            iStaNo = t.First().sta_n,
                            State = ChangedEquipState.Edit
                        }));
                        AddSetParms(deficiencySetParms);
                    }
                    editEquips = editEquips.Distinct().ToList();
                }
                //查询缺失信息
                List<object> deficiencyData = DeficiencyData(equips);
                List<YcpDto> deficiencyAddYcps = new List<YcpDto>();
                List<YxpDto> deficiencyAddYxps = new List<YxpDto>();
                List<SetParmDto> deficiencyAddSetParms = new List<SetParmDto>();
                if (deficiencyData.Count() > 0)
                {
                    //获取设备分组
                    EGroupDto eGroup = GetEGroup(iotequip_nm);
                    equipAddFlag = true;
                    //添加缺失的设备
                    for (int i = 0; i < deficiencyData.Count; i++)
                    {
                        //添加设备
                        EquipDto equip = AddEquip(i, deficiencyData[i], iotEquip);
                        addEquips.Add(new ChangedEquip()
                        {
                            iEqpNo = equip.equip_no,
                            iStaNo = equip.sta_n,
                            State = ChangedEquipState.Add
                        });
                        eGroupLists.Add(new EGroupListDto()
                        {
                            GroupId = eGroup.GroupId,
                            EquipNo = equip.equip_no,
                            StaNo = 1
                        });
                        //存储
                        deficiencyAddYcps.AddRange(GetAddEquipYcps(equip, iotEquip, iotYcps));
                        deficiencyAddYxps.AddRange(GetAddEquipYxps(equip, iotEquip, iotYxps));
                        deficiencyAddSetParms.AddRange(GetAddEquipSetParms(equip, iotEquip, iotSetParms));
                    }
                    //入库
                    AddYcps(deficiencyAddYcps);
                    AddYxps(deficiencyAddYxps);
                    AddSetParms(deficiencyAddSetParms);
                    AddGroupLists(eGroupLists);
                }
                //获取需要移除的设备
                List<EquipDto> deleteEquips = DeleteData(equips);
                if (deleteEquips != null && deleteEquips.Count > 0)
                {
                    DeleteEquips(deleteEquips);
                }
                //移除引导设备
                DeleteGuidance(equips);
                //设备实时变更
                if (equipAddFlag && !equipUpdateFlag)
                {
                    Task.Run(() =>
                    {
                        foreach (var Eqp in addEquips)
                        {
                            StationItem.AddChangedEquip(Eqp);
                        }
                    });
                }
                else if (equipUpdateFlag)
                {
                    Task.Run(() =>
                    {
                        foreach (var Eqp in editEquips)
                        {
                            StationItem.AddChangedEquip(Eqp);
                        }
                    });
                }
            }
        }
        #region 获取操作
        /// <summary>
        /// 获取产品
        /// </summary>
        protected abstract IotEquipDto GetIotEquip();
        /// <summary>
        /// 获取产品属性
        /// </summary>
        protected abstract (List<IotYcpDto>, List<IotYxpDto>, List<IotSetParmDto>) GetIotEquipProperty(int equip_no);
        /// <summary>
        /// 获取当前产品下的所有设备
        /// </summary>
        /// <returns></returns>
        protected abstract List<EquipDto> GetEquips(int equip_no);
        /// <summary>
        /// 获取设备属性集合列表
        /// </summary>
        /// <param name="equip_nos"></param>
        /// <returns></returns>
        protected abstract (List<YcpDto>, List<YxpDto>, List<SetParmDto>) GetEquipsProperty(List<int> equip_nos);
        /// <summary>
        /// 获取缺失的设备源数据
        /// </summary>
        /// <returns></returns>
        protected abstract List<object> DeficiencyData(List<EquipDto> equips);
        /// <summary>
        /// 获取需要删除的设备
        /// </summary>
        /// <param name="equips"></param>
        /// <returns></returns>
        protected abstract List<EquipDto> DeleteData(List<EquipDto> equips);
        /// <summary>
        /// 获得设备分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        protected abstract EGroupDto GetEGroup(string groupName);
        /// <summary>
        /// 获取新增遥测
        /// </summary>
        /// <param name="iotYcps"></param>
        /// <param name="iotEquips"></param>
        /// <param name="equip"></param>
        protected abstract List<YcpDto> GetAddEquipYcps(EquipDto equip, IotEquipDto iotEquips, List<IotYcpDto> iotYcps);
        /// <summary>
        /// 获取新增遥信
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="iotEquips"></param>
        /// <param name="iotYxps"></param>
        /// <returns></returns>
        protected abstract List<YxpDto> GetAddEquipYxps(EquipDto equip, IotEquipDto iotEquips, List<IotYxpDto> iotYxps);
        /// <summary>
        /// 获取新增设置
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="iotEquips"></param>
        /// <param name="iotSetParms"></param>
        /// <returns></returns>
        protected abstract List<SetParmDto> GetAddEquipSetParms(EquipDto equip, IotEquipDto iotEquips, List<IotSetParmDto> iotSetParms);
        #endregion
        #region 更新操作
        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected abstract EquipDto AddEquip(int i, object obj, IotEquipDto iotEquips);
        /// <summary>
        /// 移除设备
        /// </summary>
        /// <param name="equips"></param>
        protected abstract void DeleteEquips(List<EquipDto> equips);
        /// <summary>
        /// 新增设备分组
        /// </summary>
        /// <param name="eGroupLists"></param>
        protected abstract void AddGroupLists(List<EGroupListDto> eGroupLists);
        /// <summary>
        /// 添加遥测
        /// </summary>
        protected abstract void AddYcps(List<YcpDto> list);
        /// <summary>
        /// 添加遥信
        /// </summary>
        protected abstract void AddYxps(List<YxpDto> list);
        /// <summary>
        /// 添加设置
        /// </summary>
        protected abstract void AddSetParms(List<SetParmDto> list);
        /// <summary>
        /// 删除引导设备
        /// </summary>
        protected abstract void DeleteGuidance(List<EquipDto> equips);
        #endregion
    }
    //DTO对象
    public class IotEquipDto
    {
        public int sta_n { get; set; }
        public int equip_no { get; set; }
        public string equip_nm { get; set; }
        public string equip_detail { get; set; }
        public int acc_cyc { get; set; }
        public string related_pic { get; set; }
        public string proc_advice { get; set; }
        public string out_of_contact { get; set; }
        public string contacted { get; set; }
        public string event_wav { get; set; }
        public string communication_drv { get; set; }
        public string local_addr { get; set; }
        public string equip_addr { get; set; }
        public string communication_param { get; set; }
        public string communication_time_param { get; set; }
        public int raw_equip_no { get; set; }
        public string tabname { get; set; }
        public int alarm_scheme { get; set; }
        public int attrib { get; set; }
        public string sta_IP { get; set; }
        public int? AlarmRiseCycle { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string related_video { get; set; }
        public string ZiChanID { get; set; }
        public string PlanNo { get; set; }
        public string SafeTime { get; set; }
        public string backup { get; set; }
        /// <summary>
        /// 设备连接类型 
        /// </summary>
        public int? EquipConnType { get; set; }
    }
    public class IotYcpDto
    {
        public int sta_n { get; set; }
        public int equip_no { get; set; }
        public int yc_no { get; set; }
        public string yc_nm { get; set; }
        public int mapping { get; set; }
        public double yc_min { get; set; }
        public double yc_max { get; set; }
        public double physic_min { get; set; }
        public double physic_max { get; set; }
        public double val_min { get; set; }
        public double restore_min { get; set; }
        public double restore_max { get; set; }
        public double val_max { get; set; }
        public int val_trait { get; set; }
        public string main_instruction { get; set; }
        public string minor_instruction { get; set; }
        public int alarm_acceptable_time { get; set; }
        public int restore_acceptable_time { get; set; }
        public int alarm_repeat_time { get; set; }
        public string proc_advice { get; set; }
        public int lvl_level { get; set; }
        public string outmin_evt { get; set; }
        public string outmax_evt { get; set; }
        public string wave_file { get; set; }
        public string related_pic { get; set; }
        public int alarm_scheme { get; set; }
        /// <summary>
        /// 是否记录曲线
        /// </summary>
        public bool curve_rcd { get; set; }
        /// <summary>
        /// 记录曲线阈值
        /// </summary>
        public double? curve_limit { get; set; }
        /// <summary>
        /// 报警屏蔽
        /// </summary>
        public string alarm_shield { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 报警升级周期
        /// </summary>
        public int? AlarmRiseCycle { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string related_video { get; set; }
        public string ZiChanID { get; set; }
        public string PlanNo { get; set; }
        public string SafeTime { get; set; }
        public DateTime? safe_bgn { get; set; }
        public DateTime? safe_end { get; set; }
        public string yc_code { get; set; }

    }
    public class IotYxpDto
    {
        /// <summary>
        /// 站点号
        /// </summary>
        public int sta_n { get; set; }
        public int equip_no { get; set; }
        public int yx_no { get; set; }
        public string yx_nm { get; set; }
        /// <summary>
        /// 处理意见0-1
        /// </summary>
        public string proc_advice_r { get; set; }

        /// <summary>
        /// 处理意见1-0
        /// </summary>
        public string proc_advice_d { get; set; }
        public int level_r { get; set; }
        public int level_d { get; set; }
        public string evt_01 { get; set; }
        public string evt_10 { get; set; }
        public string main_instruction { get; set; }
        public string minor_instruction { get; set; }
        public int alarm_acceptable_time { get; set; }
        public int restore_acceptable_time { get; set; }
        public int alarm_repeat_time { get; set; }
        public string wave_file { get; set; }
        public string related_pic { get; set; }
        public int alarm_scheme { get; set; }
        public int inversion { get; set; }
        public int initval { get; set; }
        public int val_trait { get; set; }
        public string alarm_shield { get; set; }
        public int? AlarmRiseCycle { get; set; }
        public string related_video { get; set; }
        public string ZiChanID { get; set; }
        public string PlanNo { get; set; }
        public string SafeTime { get; set; }
        public bool curve_rcd { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public DateTime? safe_bgn { get; set; }
        public DateTime? safe_end { get; set; }
        public string yx_code { get; set; }
    }
    public class IotSetParmDto
    {
        public int sta_n { get; set; }
        public int equip_no { get; set; }
        public int set_no { get; set; }
        public string set_nm { get; set; }
        public string set_type { get; set; }
        public string main_instruction { get; set; }
        public string minor_instruction { get; set; }
        public bool record { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public bool canexecution { get; set; }
        public string VoiceKeys { get; set; }
        public bool EnableVoice { get; set; }
        public int qr_equip_no { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string set_code { get; set; }
    }
    public class EquipDto
    {
        public int sta_n { get; set; }
        public int equip_no { get; set; }
        public string equip_nm { get; set; }
        public string equip_detail { get; set; }
        public int acc_cyc { get; set; }
        public string related_pic { get; set; }
        public string proc_advice { get; set; }
        public string out_of_contact { get; set; }
        public string contacted { get; set; }
        public string event_wav { get; set; }
        public string communication_drv { get; set; }
        public string local_addr { get; set; }
        public string equip_addr { get; set; }
        public string communication_param { get; set; }
        public string communication_time_param { get; set; }
        public int raw_equip_no { get; set; }
        public string tabname { get; set; }
        public int alarm_scheme { get; set; }
        public int attrib { get; set; }
        public string sta_IP { get; set; }
        public int? AlarmRiseCycle { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string related_video { get; set; }
        public string ZiChanID { get; set; }
        public string PlanNo { get; set; }
        public string SafeTime { get; set; }
        public string backup { get; set; }
    }
    public class YcpDto
    {
        public int sta_n { get; set; }
        //[Key, Column(Order = 1)]
        public int equip_no { get; set; }
        //[Key]
        public int yc_no { get; set; }
        public string yc_nm { get; set; }
        public int mapping { get; set; }
        public double yc_min { get; set; }
        public double yc_max { get; set; }
        public double physic_min { get; set; }
        public double physic_max { get; set; }
        public double val_min { get; set; }
        public double restore_min { get; set; }
        public double restore_max { get; set; }
        public double val_max { get; set; }
        public int val_trait { get; set; }
        public string main_instruction { get; set; }
        public string minor_instruction { get; set; }
        public int alarm_acceptable_time { get; set; }
        public int restore_acceptable_time { get; set; }
        public int alarm_repeat_time { get; set; }
        public string proc_advice { get; set; }
        public int lvl_level { get; set; }
        public string outmin_evt { get; set; }
        public string outmax_evt { get; set; }
        public string wave_file { get; set; }
        public string related_pic { get; set; }
        public int alarm_scheme { get; set; }

        /// <summary>
        /// 是否记录曲线
        /// </summary>
        public bool curve_rcd { get; set; }

        /// <summary>
        /// 记录曲线阈值
        /// </summary>
        public double? curve_limit { get; set; }

        /// <summary>
        /// 报警屏蔽
        /// </summary>
        public string alarm_shield { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 报警升级周期
        /// </summary>
        public int? AlarmRiseCycle { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string related_video { get; set; }
        public string ZiChanID { get; set; }
        public string PlanNo { get; set; }
        public string SafeTime { get; set; }
        public DateTime? safe_bgn { get; set; }
        public DateTime? safe_end { get; set; }
        public string GWValue { get; set; }
        public DateTime? GWTime { get; set; }
        public string yc_code { get; set; }
    }
    public class YxpDto
    {
        /// <summary>
        /// 站点号
        /// </summary>
        public int sta_n { get; set; }
        public int equip_no { get; set; }
        public int yx_no { get; set; }
        public string yx_nm { get; set; }
        /// <summary>
        /// 处理意见0-1
        /// </summary>
        public string proc_advice_r { get; set; }
        /// <summary>
        /// 处理意见1-0
        /// </summary>
        public string proc_advice_d { get; set; }
        public int level_r { get; set; }
        public int level_d { get; set; }
        public string evt_01 { get; set; }
        public string evt_10 { get; set; }
        public string main_instruction { get; set; }
        public string minor_instruction { get; set; }
        public int alarm_acceptable_time { get; set; }
        public int restore_acceptable_time { get; set; }
        public int alarm_repeat_time { get; set; }
        public string wave_file { get; set; }
        public string related_pic { get; set; }
        public int alarm_scheme { get; set; }
        public int inversion { get; set; }
        public int initval { get; set; }
        public int val_trait { get; set; }
        public string alarm_shield { get; set; }
        public int? AlarmRiseCycle { get; set; }
        public string related_video { get; set; }
        public string ZiChanID { get; set; }
        public string PlanNo { get; set; }
        public string SafeTime { get; set; }
        public bool curve_rcd { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public DateTime? safe_bgn { get; set; }
        public DateTime? safe_end { get; set; }
        public string GWValue { get; set; }
        public DateTime? GWTime { get; set; }
        public string yx_code { get; set; }
    }
    public class SetParmDto
    {
        public int sta_n { get; set; }
        public int equip_no { get; set; }
        public int set_no { get; set; }
        public string set_nm { get; set; }
        public string set_type { get; set; }
        public string main_instruction { get; set; }
        public string minor_instruction { get; set; }
        public bool record { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public bool canexecution { get; set; }
        public string VoiceKeys { get; set; }
        public bool EnableVoice { get; set; }
        public int qr_equip_no { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string set_code { get; set; }
    }
    public class EGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ParentGroupId { get; set; }

    }
    public class EGroupListDto
    {
        public int EGroupListId { get; set; }
        public int GroupId { get; set; }
        public int EquipNo { get; set; }
        public int StaNo { get; set; }
    }
}
