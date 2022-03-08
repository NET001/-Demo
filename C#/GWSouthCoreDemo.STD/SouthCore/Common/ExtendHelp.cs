using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace SouthCore.Common
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class ExtendHelp
    {
        /// <summary>
        /// string分隔扩展
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <param name="defaultNull"></param>
        /// <returns></returns>
        public static string[] TryDefaultSplit(this string str, char separator, int defaultNull)
        {
            try
            {
                List<string> result = str.Split(separator).ToList();
                int count = result.Count;
                for (int i = 0; i < defaultNull - count; i++)
                {
                    result.Add("");
                }
                return result.ToArray();
            }
            catch
            {
                List<string> result = new List<string>();
                for (int i = 0; i < defaultNull; i++)
                {
                    result.Add("");
                }
                return result.ToArray();
            }
        }

        /// <summary>
        /// 使用反射将目标对象进行转换支持List转(简单的同名转换)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">源对象</param>
        /// <returns></returns>
        public static T ToTransition<T>(this object obj, List<(string, string, Func<object, object>)> maps = null) where T : class
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
        /// <summary>
        /// 获得上一级路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParentPath(this string path)
        {
            string[] pathSplit = path.Split('/');
            string result = "";
            for (int i = 0; i < pathSplit.Length - 1; i++)
            {
                result += pathSplit[i] + "/";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }
        public static string ToyyyyMMddHHmmss13Timestamp(this string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow).ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ToyyyyMMddHHmmss(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
