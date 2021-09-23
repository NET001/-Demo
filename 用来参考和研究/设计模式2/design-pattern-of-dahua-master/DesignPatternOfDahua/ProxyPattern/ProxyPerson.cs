using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    class ProxyPerson : DoSomething
    {
        BasePerson basePerson;
        public void SendChocolates()
        {
            if (basePerson == null)
            {
                basePerson = new BasePerson();
            }
            basePerson.SendChocolates();
        }

        public void SendFlowers()
        {
            if (basePerson == null)
            {
                basePerson = new BasePerson();
            }
            basePerson.SendFlowers();
        }

        public void WatchMovie()
        {
            if (basePerson == null)
            {
                basePerson = new BasePerson();
            }
            basePerson.WatchMovie();
        }
    }
}
