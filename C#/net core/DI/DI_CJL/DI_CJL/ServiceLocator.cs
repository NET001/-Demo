using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DI_CJL
{
    public interface IServiceLocator
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Implementation"></param>
        void AddService<T>();
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <typeparam name="BaseT"></typeparam>
        /// <typeparam name="T"></typeparam>
        void AddService<BaseT, T>();
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>();
    }
    public class ServiceLocator : IServiceLocator
    {
        private static ServiceLocator _instance;
        private static readonly object _locker = new object();
        private readonly IDictionary<Type, Type> servicesType;
        private readonly IDictionary<Type, object> instantiatedServices;

        public static ServiceLocator GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new ServiceLocator();
                    }
                }
            }
            return _instance;
        }

        private ServiceLocator()
        {
            //配置
            servicesType = new ConcurrentDictionary<Type, Type>();
            //实例
            instantiatedServices = new ConcurrentDictionary<Type, object>();
        }
        public void AddService<T>()
        {
            servicesType[typeof(T)] = typeof(T);
        }
        public void AddService<BaseT, T>()
        {
            servicesType[typeof(BaseT)] = typeof(T);
        }
        public T GetService<T>()
        {
            var service = (T)GetService(typeof(T));
            if (service == null)
            {
                throw new ApplicationException("请求的服务未注册");
            }
            return service;
        }
        /// <summary>
        /// 获取服务对象,支持延迟对象创建
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object GetService(Type type)
        {
            if (!instantiatedServices.ContainsKey(type))
            {
                try
                {
                    ConstructorInfo constructor = servicesType[type].GetTypeInfo().DeclaredConstructors
                                                .Where(constructor => constructor.IsPublic).FirstOrDefault();
                    ParameterInfo[] ps = constructor.GetParameters();
                    List<object> parameters = new List<object>();
                    for (int i = 0; i < ps.Length; i++)
                    {
                        ParameterInfo item = ps[i];
                        bool done = instantiatedServices.TryGetValue(item.ParameterType, out object parameter);
                        if (!done)
                        {
                            parameter = GetService(item.ParameterType);
                        }
                        parameters.Add(parameter);
                    }
                    object service = constructor.Invoke(parameters.ToArray());
                    instantiatedServices.Add(type, service);
                }
                catch (KeyNotFoundException)
                {
                    throw new ApplicationException("请求的服务未注册");
                }
            }
            return instantiatedServices[type];
        }

    }
}
