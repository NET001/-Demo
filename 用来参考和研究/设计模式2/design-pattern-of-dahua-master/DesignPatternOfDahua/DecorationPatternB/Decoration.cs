using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternB
{
    public class Decoration : Person
    {
        protected Person person;

        public void SetPerson(Person person)
        {
            this.person = person;
        }
        public override void Show()
        {
            //base.Show();
            person.Show();
        }
    }
}
