using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter_CJL
{
    public class Context
    {
        private string input;
        public string Input
        {
            get { return input; }
            set { input = value; }
        }

        private string output;
        public string Output
        {
            get { return output; }
            set { output = value; }
        }
    }

}
