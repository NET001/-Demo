using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoPattern
{
    public class Memo
    {
        public string State { get; }

        public Memo(string state)
        {
            State = state;
        }
    }
}
