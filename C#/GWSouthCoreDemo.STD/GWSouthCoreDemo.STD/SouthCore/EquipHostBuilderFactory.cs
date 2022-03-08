using SouthCore.Core;
using SouthCore.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public static class SouthHostBuilderFactory
    {
        public static ISouthHostBuilder CreateSouthHostBuilder(SouthHostBuilderFactoryEnum equip = SouthHostBuilderFactoryEnum.Default)
        {
            ISouthHostBuilder resul = null;
            switch (equip)
            {
                case SouthHostBuilderFactoryEnum.Default:
                    {
                        resul = new DefaultSouthHostBuilder();
                    }
                    break;
            }
            return resul;
        }
    }
    public enum SouthHostBuilderFactoryEnum
    {
        Default,
    }
}
