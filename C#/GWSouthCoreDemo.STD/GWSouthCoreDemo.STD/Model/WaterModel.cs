using System;
using System.Collections.Generic;
using System.Text;

namespace GWSouthCoreDemo.STD.Model
{
    public class WaterModel
    {
        public string waterCode { get; set; }
        public string waterName { get; set; }
        public string flow { get; set; }
        public string currentTime { get; set; }
        public string totalValue { get; set; }
        public string area { get; set; }
    }
    public class WaterModelResult
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<WaterModel> content { get; set; }
    }
}
