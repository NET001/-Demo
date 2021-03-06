using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypePattern_CJL
{
    /// <summary>
    /// 具体原型类1
    /// </summary>
   public class ConcretePrototype1 : Prototype
    {
        // Constructor 
        public ConcretePrototype1(string id)
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
