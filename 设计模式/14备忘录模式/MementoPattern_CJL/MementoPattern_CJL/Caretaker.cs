﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MementoPattern_CJL
{
    public class Caretaker
    {
        private Memento memento;

        public Memento Memento
        {
            get { return memento; }
            set { memento = value; }
        }
    }
}
