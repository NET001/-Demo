using Abp.Modules;
using Abp.Reflection.Extensions;
using Magicodes.Admin.Core;

namespace Magicodes.Admin.Unity
{
    [DependsOn(
        typeof(AdminCoreModule))]
    public class UnityModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(UnityModule).GetAssembly());
        }

        public override void PostInitialize()
        {
           
        }
    }
}