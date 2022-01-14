using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EventBusAttribute_CJL
{
    public class EventBus
    {
        private List<object> observers = new List<object>();
        public void Attach(object obj)
        {
            observers.Add(obj);
        }
        public void Detach(object obj)
        {
            observers.Remove(obj);
        }
        public void Notify(object parameter)
        {
            foreach (var observer in observers)
            {
                Type type = observer.GetType();
                MethodInfo[] methods = type.GetMethods();
                foreach (var method in methods)
                {
                    EventBusFunctionAttribute attribute = method.GetCustomAttribute<EventBusFunctionAttribute>();
                    if (attribute != null)
                    {
                        if (attribute.ParameterType == parameter.GetType())
                        {
                            method.Invoke(observer, new object[] { parameter });
                        }
                    }
                }
            }
        }
    }
}
