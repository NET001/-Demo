using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExteriorPattern
{
    class ExteriorMethod
    {
        AchieveA achieveA;
        AchieveB achieveB;
        AchieveC achieveC;

        public void Created()
        {
            achieveA = new AchieveA();
            achieveB = new AchieveB();
            achieveC = new AchieveC();
        }

        public void Show()
        {
            achieveA.AchiveMethodA();
            achieveB.AchiveMethodB();
            achieveC.AchiveMethodC();
        }
    }
}
