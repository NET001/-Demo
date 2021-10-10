﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern_CJL
{
    public abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }

}
