using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    class ObjectStructrue
    {
        private IList<Faction> factions = new List<Faction>();

        public void Add(Faction faction)
        {
            factions.Add(faction);
        }

        public void Delete(Faction faction)
        {
            factions.Remove(faction);
        }

        public void Display(State state)
        {
            foreach (var item in factions)
            {
                item.Accept(state);
            }
        }
    }
}