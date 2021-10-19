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
        void AddService<T>();

        //新增接口
        void AddService<T>(Func<T> Implementation);

        T GetService<T>();
    }

    public class ServiceLocator : IServiceLocator
    {
        private static ServiceLocator _instance;

        private static readonly object _locker = new object();

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

        private readonly IDictionary<Type, Type> servicesType;
        private readonly IDictionary<Type, object> instantiatedServices;

        private ServiceLocator()
        {
            servicesType = new ConcurrentDictionary<Type, Type>();
            instantiatedServices = new ConcurrentDictionary<Type, object>();
        }

        public void AddService<T>()
        {
            servicesType.Add(typeof(T), typeof(T));
        }
        public void AddService<T>(Func<T> Implementation)
        {
            servicesType.Add(typeof(T), typeof(T));
            var done = instantiatedServices.TryAdd(typeof(T), Implementation());
            Debug.Assert(done, "Cannot add current service: " + typeof(T));
        }

        public T GetService<T>()
        {
            var service = (T)GetService(typeof(T));
            if (service == null)
            {
                throw new ApplicationException("The requested service is not registered");
            }
            return service;
        }

        /// <summary>
        /// 关键代码
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
                    throw new ApplicationException("The requested service is not registered");
                }
            }
            return instantiatedServices[type];
        }
    }
}
