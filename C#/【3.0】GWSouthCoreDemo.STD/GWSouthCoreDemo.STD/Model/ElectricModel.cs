using System;
using System.Collections.Generic;
using System.Text;

namespace GWSouthCoreDemo.STD.Model
{
    public class ElectricModel
    {
        public string electricCode { get; set; }
        public string electricName { get; set; }
        public string quantity { get; set; }
        public string area { get; set; }
        public string type { get; set; }
    }
    public class ElectricModelResult
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<ElectricModel> content { get; set; }
    }
}
