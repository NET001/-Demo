using System;
using System.Collections.Generic;
using System.Text;

namespace MementoPattern_CJL
{
    /// <summary>
    /// 备忘录
    /// </summary>
    public class Memento
    {
        private string state;

        public Memento(string state)
        {
            this.state = state;
        }

        public string State
        {
            get { return state; }
        }
    }

}
