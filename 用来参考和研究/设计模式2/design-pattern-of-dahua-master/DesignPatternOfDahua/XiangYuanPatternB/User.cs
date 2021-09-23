using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangYuanPatternB
{
    class User
    {
        private string name { get; set; }

        public User(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
        }
    }
}
