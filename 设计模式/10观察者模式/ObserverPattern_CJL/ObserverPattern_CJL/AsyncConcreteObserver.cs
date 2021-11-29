using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ObserverPattern_CJL
{
    /// <summary>
    /// 具体观察者
    /// </summary>
    public class AsyncConcreteObserver : Observer
    {
        private string name;
        private string observerState;
        private AsyncConcreteSubject subject;

        public AsyncConcreteObserver(AsyncConcreteSubject subject, string name)
        {
            this.subject = subject;
            this.name = name;
        }
        //更新
        public override void Update()
        {
            Thread.Sleep(1000);
            observerState = subject.SubjectState;
            Console.WriteLine("观察者{0}的新状态是{1}", name, observerState);
        }
        public AsyncConcreteSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
}
