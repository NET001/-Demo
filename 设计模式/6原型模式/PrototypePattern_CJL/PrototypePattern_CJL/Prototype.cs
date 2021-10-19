using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypePattern_CJL
{
    /// <summary>
    /// 抽象原型类
    /// </summary>
    public abstract class Prototype
    {
        private string id;

        // Constructor 
        public Prototype(string id)
        {
            this.id = id;
        }
        // Property 
        public string Id
        {
            get { return id; }
        }
        public abstract Prototype Clone();
    }

}
