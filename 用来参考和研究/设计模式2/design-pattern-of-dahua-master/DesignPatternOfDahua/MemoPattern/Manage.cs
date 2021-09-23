using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoPattern
{
    public class Manage
    {
        public Memo memo { get; set; }

        public Manage(Memo memo)
        {
            this.memo = memo;
        }
    }
}
