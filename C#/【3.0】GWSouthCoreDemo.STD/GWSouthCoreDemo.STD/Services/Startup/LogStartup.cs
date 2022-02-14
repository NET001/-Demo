using GWDataCenter;
using GWSouthCoreDemo.STD.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SouthCore.Default;
using SouthCore.Default.Log;

namespace GWSouthCoreDemo.STD.Services
{
    //日志实现
    public class LogStartup
    {
        public void ConfigureServices(IDefaultAppEquipBuilder builder)
        {
            builder.SetLogs(GlobalModel.DllName, logs => logs
                .SetLog((string log) =>
                    {
                        DataCenter.WriteLogFile(log);
                    })
            );
        }
    }
}
