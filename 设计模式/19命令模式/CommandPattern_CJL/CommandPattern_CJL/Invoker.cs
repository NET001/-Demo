﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern_CJL
{
    /// <summary>
    /// 请求者/调用者
    /// </summary>
    public class Invoker
    {
        private Command command;

        public void SetCommand(Command command)
        {
            this.command = command;
        }

        public void ExecuteCommand()
        {
            command.Execute();
        }
    }

}
