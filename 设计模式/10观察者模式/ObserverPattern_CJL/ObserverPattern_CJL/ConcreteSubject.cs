using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ObserverPattern_CJL
{
    /// <summary>
    ///具体主题
    /// </summary>
    public class ConcreteSubject : Subject
    {
        private string subjectState;

        //具体通知者状态
        public string SubjectState
        {
            get { return subjectState; }
            set { subjectState = value; }
        }

        public override void Notify()
        {
            //执行耗时操作
            Thread.Sleep(1000);
            base.Notify();
            Console.WriteLine("具体主题发送完成了通知");
        }
    }
}
