using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Core
{
    public interface ISouthHostBuilder
    {
        IDictionary<string, object> Services { get; }
        ISouthHost Build();
    }
}
