using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Core
{
    public interface ISouthHost
    {
        IDictionary<string, object> Services { get; }
        void Run();
    }
}
