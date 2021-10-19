﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern_CJL
{
    /// <summary>
    /// 客户端
    /// </summary>
   public class Client
    {
        private AbstractProductA AbstractProductA;
        private AbstractProductB AbstractProductB;

        // Constructor 
        public Client(AbstractFactory factory)
        {
            AbstractProductB = factory.CreateProductB();
            AbstractProductA = factory.CreateProductA();
        }

        public void Run()
        {
            AbstractProductB.Interact(AbstractProductA);
        }
    }
}
