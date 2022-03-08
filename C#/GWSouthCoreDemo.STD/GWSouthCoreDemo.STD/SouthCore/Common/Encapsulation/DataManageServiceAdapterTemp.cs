using IoTCustomDataCore.Entity;
using IoTCustomDataCore.IoC;
using IoTCustomDataCore.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IoTCustomDataCore.CustomDataEntity;

namespace SouthCore.Common
{
    /// <summary>
    /// 数据库操作适配器模板
    /// </summary>
    public class DataManageServiceAdapterTemp : IDataManageService
    {
        private static TianJianGangManageService dataService = InjectionContext.GetService<TianJianGangManageService>();
        public List<T> GetList<T>(Func<T, bool> predicate) where T : class
        {
            if (typeof(T) == typeof(IotEquipDto))
            {
                return dataService.GetList<IotEquip>(new Func<IotEquip, bool>((IotEquip obj) =>
                {
                    return predicate(obj.ToTransition<IotEquipDto>() as T);
                })).ToTransition<List<IotEquipDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(IotYcpDto))
            {
                return dataService.GetList<IotYcp>(new Func<IotYcp, bool>((IotYcp obj) =>
                {
                    return predicate(obj.ToTransition<IotYcpDto>() as T);
                })).ToTransition<List<IotYcpDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(IotYxpDto))
            {
                return dataService.GetList<IotYxp>(new Func<IotYxp, bool>((IotYxp obj) =>
                {
                    return predicate(obj.ToTransition<IotYxpDto>() as T);
                })).ToTransition<List<IotYxpDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(IotSetParmDto))
            {
                return dataService.GetList<IotSetParm>(new Func<IotSetParm, bool>((IotSetParm obj) =>
                {
                    return predicate(obj.ToTransition<IotSetParmDto>() as T);
                })).ToTransition<List<IotSetParmDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(EquipDto))
            {
                return dataService.GetList<Equip>(new Func<Equip, bool>((Equip obj) =>
                {
                    return predicate(obj.ToTransition<EquipDto>() as T);
                })).ToTransition<List<EquipDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(YcpDto))
            {
                return dataService.GetList<Ycp>(new Func<Ycp, bool>((Ycp obj) =>
                {
                    return predicate(obj.ToTransition<YcpDto>() as T);
                })).ToTransition<List<YcpDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(YxpDto))
            {
                return dataService.GetList<Yxp>(new Func<Yxp, bool>((Yxp obj) =>
                {
                    return predicate(obj.ToTransition<YxpDto>() as T);
                })).ToTransition<List<YxpDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(SetParmDto))
            {
                return dataService.GetList<SetParm>(new Func<SetParm, bool>((SetParm obj) =>
                {
                    return predicate(obj.ToTransition<SetParmDto>() as T);
                })).ToTransition<List<SetParmDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(EGroupDto))
            {
                return dataService.GetList<EGroup>(new Func<EGroup, bool>((EGroup obj) =>
                {
                    return predicate(obj.ToTransition<EGroupDto>() as T);
                })).ToTransition<List<EGroupDto>>().Select(t => t as T).ToList();
            }
            else if (typeof(T) == typeof(EGroupListDto))
            {
                return dataService.GetList<EGroupList>(new Func<EGroupList, bool>((EGroupList obj) =>
                {
                    return predicate(obj.ToTransition<EGroupListDto>() as T);
                })).ToTransition<List<EGroupListDto>>().Select(t => t as T).ToList();
            }
            else
            {
                return dataService.GetList<T>(predicate);
            }
        }
        public T Add<T>(T obj) where T : class
        {
            if (obj is EquipDto)
            {
                Equip _obj = obj.ToTransition<Equip>();
                dataService.Add(_obj);
                return _obj.ToTransition<EquipDto>() as T;
            }
            else if (obj is EGroupDto)
            {
                EGroup _obj = obj.ToTransition<EGroup>();
                dataService.Add(_obj);
                return _obj.ToTransition<EGroupDto>() as T;
            }
            else
            {
                return dataService.Add<T>(obj);
            }
        }
        public int AddList<T>(List<T> equipList) where T : class
        {
            if (equipList is List<YcpDto>)
            {
                List<Ycp> _obj = equipList.ToTransition<List<Ycp>>();
                return dataService.AddList(_obj);
            }
            else if (equipList is List<YxpDto>)
            {
                List<Yxp> _obj = equipList.ToTransition<List<Yxp>>();
                return dataService.AddList(_obj);
            }
            else if (equipList is List<SetParmDto>)
            {
                List<SetParm> _obj = equipList.ToTransition<List<SetParm>>();
                return dataService.AddList(_obj);
            }
            else if (equipList is List<EGroupListDto>)
            {
                List<EGroupList> _obj = equipList.ToTransition<List<EGroupList>>();
                return dataService.AddList(_obj);
            }
            else
            {
                return dataService.AddList<T>(equipList);
            }
        }
        public int Delete<T>(T obj)
        {
            if (obj is EquipDto)
            {
                Equip _obj = obj.ToTransition<Equip>();
                return dataService.Delete(_obj);
            }
            else
            {
                return dataService.Delete<T>(obj);
            }
        }
        public int DeleteList<T>(List<T> list) where T : class
        {
            if (typeof(T) == typeof(EquipDto))
            {
                List<Equip> equips = list.ToTransition<List<Equip>>();
                return dataService.DeleteList<Equip>(equips);
            }
            else
            {
                return dataService.DeleteList<T>(list);
            }
        }
    }
}
