using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ObserverPattern_CJL
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
    /// <summary>
    /// 用于标记观察者执行的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EventBusFunctionAttribute : Attribute
    {
        private Type parameterType;
        public Type ParameterType { get { return parameterType; } }
        public EventBusFunctionAttribute(Type parameterType)
        {
            this.parameterType = parameterType;
        }
    }
    public class Subject1
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
    }
    public class Concrete1Observer
    {
        [EventBusFunction(typeof(Subject1))]
        public void Update(object obj)
        {
            Subject1 subject1 = obj as Subject1;
            Console.WriteLine("Concrete1Observer接收到数据" + subject1.c1 + "," + subject1.c2);
        }
    }
    public class Subject2
    {
        public string c3 { get; set; }
        public string c4 { get; set; }
    }
    public class Concrete2Observer
    {
        [EventBusFunction(typeof(Subject2))]
        public void Operation(object obj)
        {
            Subject2 subject2 = obj as Subject2;
            Console.WriteLine("Concrete2Observer接收到数据" + subject2.c3 + "," + subject2.c4);
        }
    }
}
