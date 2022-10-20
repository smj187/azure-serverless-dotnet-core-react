using CognitiveService.Core.Domain;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public class VoiceBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Voice>(x =>
            {
                x.MapProperty(x => x.DisplayName).SetElementName("display_name").SetIsRequired(true);
                x.MapProperty(x => x.InternalName).SetElementName("internal_name").SetIsRequired(true);
                x.MapProperty(x => x.Locale).SetElementName("locale").SetIsRequired(true);
                x.MapProperty(x => x.Gender).SetElementName("gender").SetIsRequired(true);
                x.MapProperty(x => x.AvatarImage).SetElementName("avatar_image").SetIsRequired(true);
                x.MapProperty(x => x.PreviewAudio).SetElementName("preview_audio").SetIsRequired(true);

                x.MapProperty(x => x.VoiceProvider).SetElementName("voice_provider").SetIsRequired(true);
                x.MapProperty(x => x.VoiceType).SetElementName("voice_type").SetIsRequired(true);
                x.MapProperty(x => x.AwsVoiceConfig).SetElementName("aws_config").SetIsRequired(false);
                x.MapProperty(x => x.GoogleVoiceConfig).SetElementName("google_config").SetIsRequired(false);
                x.MapProperty(x => x.AzureVoiceConfig).SetElementName("azure_voice_config").SetIsRequired(false);

                x.MapProperty(x => x.IsAvailable).SetElementName("is_available").SetIsRequired(true);
            });



            BsonClassMap.RegisterClassMap<AwsVoiceConfig>(x =>
            {
                x.MapProperty(x => x.SpecialStyles).SetElementName("special_styles").SetIsRequired(true);
                x.MapProperty(x => x.Engines).SetElementName("engines").SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<GoogleVoiceConfig>(x =>
            {
                x.MapProperty(x => x.LanguageCodes).SetElementName("language_codes").SetIsRequired(true);
                x.MapProperty(x => x.NaturalSampleRateHertz).SetElementName("natural_sample_rate_hertz").SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<AzureVoiceConfig>(x =>
            {
                x.MapProperty(x => x.SampleRateHertz).SetElementName("sample_rate_herz").SetIsRequired(true);
                x.MapProperty(x => x.VoiceType).SetElementName("voice_type").SetIsRequired(true);
                x.MapProperty(x => x.WordsPerMinute).SetElementName("words_per_minute").SetIsRequired(true);
                x.MapProperty(x => x.StyleList).SetElementName("style_list").SetIsRequired(true);
                x.MapProperty(x => x.RolePlayList).SetElementName("roleplay_list").SetIsRequired(true);
                x.MapProperty(x => x.IsHighQuality48K).SetElementName("is_high_quality_48k").SetIsRequired(true);
            });

     

        }
    }
}
