using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternB
{
    public class TranslatePerson : Player
    {
        public ForeignPlayer foreignPlayer;

        public TranslatePerson(string name) : base(name)
        {
        }

        public override void Attack()
        {
            foreignPlayer = new ForeignPlayer(name);
            foreignPlayer.Attack();
        }

        public override void Defense()
        {
            foreignPlayer = new ForeignPlayer(name);
            foreignPlayer.Defense();
        }
    }
}
