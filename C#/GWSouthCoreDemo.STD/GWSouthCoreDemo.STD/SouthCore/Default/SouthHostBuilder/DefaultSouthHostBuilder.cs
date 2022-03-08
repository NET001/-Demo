using SouthCore.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default
{
    public class DefaultSouthHostBuilder : ISouthHostBuilder
    {
        IDictionary<string, object> services = new Dictionary<string, object>();
        public IDictionary<string, object> Services => services;
        public ISouthHost Build()
        {
            return new DefaultSouthHost(Services);
        }
    }
}
