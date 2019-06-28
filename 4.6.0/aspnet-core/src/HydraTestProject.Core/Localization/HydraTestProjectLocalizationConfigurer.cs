using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace HydraTestProject.Localization
{
    public static class HydraTestProjectLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(HydraTestProjectConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(HydraTestProjectLocalizationConfigurer).GetAssembly(),
                        "HydraTestProject.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
