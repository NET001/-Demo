﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangYuanPattern
{
    public class ShareConcrateFlyweight : Flyweight
    {
        public override void Oparation(int param)
        {
            Console.WriteLine("共享：{0}", param);
        }
    }
}
