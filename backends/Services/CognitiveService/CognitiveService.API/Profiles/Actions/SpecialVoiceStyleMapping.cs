using AutoMapper;
using CognitiveService.Contracts.Responses;
using CognitiveService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.API.Profiles.Actions
{
    public class SpecialVoiceStyleMapping : IMappingAction<Voice, UserVoicesResponse>
    {
        public void Process(Voice source, UserVoicesResponse destination, ResolutionContext context)
        {
            if (source.VoiceProvider.IsAzureProvider())
            {
                if (source.AzureVoiceConfig != null && source.AzureVoiceConfig.StyleList != null)
                {
                    destination.SpecialStyles.AddRange(source.AzureVoiceConfig.StyleList);
                }
            }
  
            if (source.VoiceProvider.IsAwsProvider())
            {
                if (source.AwsVoiceConfig != null && source.AwsVoiceConfig.SpecialStyles != null)
                {
                    destination.SpecialStyles.AddRange(source.AwsVoiceConfig.SpecialStyles);
                }
            }

        }
    }
}
