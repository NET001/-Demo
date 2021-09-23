using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoPattern
{
    public class Originator
    {
        public string State { get; set; }

        public Memo CreateMemo()
        {
            return new Memo(State);
        }
        
        public void SetMemo(Memo memo)
        {
            State = memo.State;
        }

        public void Show()
        {
            Console.WriteLine("当前状态是：" + State);
        }
    }
}
