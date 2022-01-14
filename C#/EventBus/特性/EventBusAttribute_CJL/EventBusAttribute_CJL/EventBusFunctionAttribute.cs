using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusAttribute_CJL
{
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
}
