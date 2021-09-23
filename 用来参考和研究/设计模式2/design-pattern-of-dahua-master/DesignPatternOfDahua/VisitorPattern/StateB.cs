using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    class StateB : State
    {
        public override void getStateForFactionA(FactionA factionA)
        {
            Console.WriteLine("{0}时，派别{1}表现评分A+", this.GetType().Name, factionA.GetType().Name);
        }

        public override void getStateForFactionB(FactionB factionB)
        {
            Console.WriteLine("{0}时，派别{1}表现评分A-", this.GetType().Name, factionB.GetType().Name);
        }
    }
}