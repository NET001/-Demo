using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternB
{
    public class DecorationB : Decoration
    {
        public override void Show()
        {
            base.Show();
            Console.Write("B装扮");
        }
    }
}
