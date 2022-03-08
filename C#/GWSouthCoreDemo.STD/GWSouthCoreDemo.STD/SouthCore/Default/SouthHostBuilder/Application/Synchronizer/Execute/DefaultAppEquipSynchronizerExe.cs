using SouthCore.Core;

namespace SouthCore.Default.Synchronizer
{
    public class DefaultAppEquipSynchronizerExe : IDefaultAppEquipSynchronizerExe
    {
        private ISouthHost southHost = null;
        public ISouthHost SouthHost => southHost;
        public DefaultAppEquipSynchronizerExe(ISouthHost southHost)
        {
            this.southHost = southHost;
        }
    }
}