using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace CJLMinMap
{
    public static class ExtendHelp
    {

        public static T ToTransition<T>(this object obj, List<(string, string, Func<object, object>)> maps = null, List<(TransitionRule, object)> rule = null) where T : class
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            Type type = result.GetType();
            if (type.GetInterface("IEnumerable", true) != null)
            {
                List<object> objList = (obj as IEnumerable<object>).ToList();
                for (int i = 0; i < objList.Count(); i++)
                {
                    object genericObj = Activator.CreateInstance(type.GetGenericArguments()[0]);
                    Type genericType = genericObj.GetType();
                    Transition(objList[i], genericType, genericObj);
                    type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public).Invoke(result, new object[] { genericObj });
                };
            }
            else
            {
                Transition(obj, type, result);
            }
            return result;
            void Transition(object _obj, Type _type, object _result)
            {
                IEnumerable<(string, string, Func<object, object>)> map = null;
                _obj.GetType().GetProperties().ToList().ForEach(new Action<PropertyInfo>((PropertyInfo t) =>
                {
                    try
                    {
                        _type.GetProperties().ToList().Where(_t => new Func<string>(() =>
                        {
                            if (maps != null)
                            {
                                map = maps.Where(_t => _t.Item1 == t.Name);
                                return map.Count() > 0 ? map.First().Item2 : t.Name;
                            }
                            else
                            {
                                return t.Name;
                            }
                        })() == _t.Name)
                        .FirstOrDefault()?.SetValue(_result,
                            new Func<object>(() =>
                            {
                                if (maps != null)
                                {
                                    return map != null && map.Count() > 0 && map.First().Item3 != null ?
                                    map.First().Item3(t.GetValue(_obj)) : t.GetValue(_obj);
                                }
                                else
                                {
                                    return t.GetValue(_obj);
                                }
                            })()
                        );
                    }
                    catch
                    {

                    }
                }));
            }
        }

    }
    public enum TransitionRule
    {
        TypeConvert,
    }
}
