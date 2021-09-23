using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternB
{
    public class DecorationA : Decoration
    {
        public override void Show()
        {
            base.Show();
            Console.Write("A装扮");
        }
    }
}
