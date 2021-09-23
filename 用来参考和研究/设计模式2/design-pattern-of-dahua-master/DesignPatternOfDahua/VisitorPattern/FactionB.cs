﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    class FactionB : Faction
    {
        public override void Accept(State state)
        {
            state.getStateForFactionB(this);
        }
    }
}
