using System.IO;
using System.Linq;
using Abp.AspNetZeroCore;
using Abp.AutoMapper;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Web;
using Abp.Zero;

namespace Magicodes.Admin.Localization
{
    [DependsOn(
        typeof(AbpZeroCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpWebCommonModule),
        typeof(AbpAspNetZeroCoreModule))]
    public class LocalizationModule : AbpModule
    {
        public override void PreInitialize()
        {
            var localizationFolder = Path.Combine(Path.GetDirectoryName(typeof(LocalizationModule).Assembly.Location), "Localization");
            if (!Directory.Exists(localizationFolder)) return;

            var adminPath = Path.Combine(localizationFolder, LocalizationConsts.LocalizationSourceName);

            if (!Directory.Exists(adminPath)) return;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    LocalizationConsts.LocalizationSourceName,
                    new XmlFileLocalizationDictionaryProvider(
                        adminPath
                    )
                )
            );

            if (Configuration.Localization.Sources.Any(p => p.Name == "Abp"))
            {
                //移除Abp源,添加自己的语言定义
                Configuration.Localization.Sources.Remove(Configuration.Localization.Sources.First(p => p.Name == "Abp"));
            }

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "Abp",
                    new XmlFileLocalizationDictionaryProvider(
                        Path.Combine(localizationFolder, "Abp")
                    )
                )
            );

            if (Configuration.Localization.Sources.Any(p => p.Name == "AbpZero"))
            {
                //移除AbpZero源,添加自己的语言定义
                Configuration.Localization.Sources.Remove(Configuration.Localization.Sources.First(p => p.Name == "AbpZero"));
            }

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "AbpZero",
                    new XmlFileLocalizationDictionaryProvider(
                        Path.Combine(localizationFolder, "AbpZero")
                    )
                )
            );

            if (Configuration.Localization.Sources.Any(p => p.Name == "AbpWeb"))
            {
                //移除Abp源,添加自己的语言定义
                Configuration.Localization.Sources.Remove(
                    Configuration.Localization.Sources.First(p => p.Name == "AbpWeb"));
            }

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "AbpWeb",
                    new XmlFileLocalizationDictionaryProvider(
                        Path.Combine(localizationFolder, "AbpWeb")
                    )
                )
            );
        }

    }
}
