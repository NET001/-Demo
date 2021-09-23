using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TertiumQuid
{
    public abstract class Worker
    {
        public Medium medium { get; set; }
        public abstract void Send(string message);
    }
}