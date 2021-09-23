using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternB
{
    public class ForeignPlayer
    {
        public string ForeignName { get; set; }

        public ForeignPlayer(string foreignName)
        {
            ForeignName = foreignName;
        }

        public void Attack()
        {
            Console.WriteLine("外籍球员{0}进攻了", ForeignName);
        }

        public void Defense()
        {
            Console.WriteLine("外籍球员{0}好防守", ForeignName);
        }
    }
}
