using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypePattern_CJL
{

    /// <summary>
    /// 具体原型类2
    /// </summary>
    public class ConcretePrototype2 : Prototype
    {
        // Constructor 
        public ConcretePrototype2(string id)
            : base(id)
        {
        }
        public override Prototype Clone()
        {
            // Shallow copy 
            return (Prototype)this.MemberwiseClone();
        }
    }
}
