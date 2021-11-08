using System;

namespace Common_CJL
{
    public interface ISocketBuild
    {   /// <summary>
        /// 构建出Socket客户端
        /// </summary>
        /// <returns></returns>
        ISocketClientHelp BuildClientHelp();
        /// <summary>
        /// 构建Socket服务端
        /// </summary>
        /// <returns></returns>
        ISocketServerHelp BuildServerHelp();
    }
}
