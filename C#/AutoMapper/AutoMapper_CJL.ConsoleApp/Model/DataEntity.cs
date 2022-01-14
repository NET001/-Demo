using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper_CJL.ConsoleApp.Model
{
    public class DataEntity
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Property1 { get; set; }
        public double Property2 { get; set; }
        public object Property3 { get; set; }
        public float Property4 { get; set; }
        public decimal Property5 { get; set; }
    }
}
