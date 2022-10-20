using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Infrastructure.ProviderResponses
{
    public class AzureVoiceResponse
    {
        public AzureVoiceResponse(string name, string displayName, string localName, string shortName, string gender, string locale, string localeName, string sampleRateHertz, string voiceType, string status, string wordsPerMinute, List<string> styleList, ExtendedPropertyMap extendedPropertyMap, List<string> secondaryLocaleList, List<string> rolePlayList)
        {
            Name = name;
            DisplayName = displayName;
            LocalName = localName;
            ShortName = shortName;
            Gender = gender;
            Locale = locale;
            LocaleName = localeName;
            SampleRateHertz = sampleRateHertz;
            VoiceType = voiceType;
            Status = status;
            WordsPerMinute = wordsPerMinute;
            StyleList = styleList;
            ExtendedPropertyMap = extendedPropertyMap;
            SecondaryLocaleList = secondaryLocaleList;
            RolePlayList = rolePlayList;
        }

        public string Name { get; init; }
        public string DisplayName { get; init; }
        public string LocalName { get; init; }
        public string ShortName { get; init; }
        public string Gender { get; init; }
        public string Locale { get; init; }
        public string LocaleName { get; init; }
        public string SampleRateHertz { get; init; }
        public string VoiceType { get; init; }
        public string Status { get; init; }
        public string WordsPerMinute { get; init; }
        public List<string> StyleList { get; init; }
        public ExtendedPropertyMap ExtendedPropertyMap { get; init; }
        public List<string> SecondaryLocaleList { get; init; }
        public List<string> RolePlayList { get; init; }
    }

    public class ExtendedPropertyMap
    {
        public string IsHighQuality48K { get; init; }
    }
}
