using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoPattern
{
    /// <summary>
    /// 备忘录模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Originator originator = new Originator();
            originator.State = "Off";
            originator.Show();

            Manage manage = new Manage(originator.CreateMemo());

            originator.State = "On";
            originator.Show();

            originator.SetMemo(manage.memo);
            originator.Show();
            Console.ReadKey();
        }
    }
}
