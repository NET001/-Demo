using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
