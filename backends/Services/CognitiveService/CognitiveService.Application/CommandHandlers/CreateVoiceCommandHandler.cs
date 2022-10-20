using CognitiveService.Application.Commands;
using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.CommandHandlers
{
    public class CreateVoiceCommandHandler : IRequestHandler<CreateVoiceCommand, Voice>
    {
        private readonly IVoiceRepository _voiceRepository;

        public CreateVoiceCommandHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<Voice> Handle(CreateVoiceCommand request, CancellationToken cancellationToken)
        {
            Voice newVoice;

            // check if internal name is available
            if (await _voiceRepository.Exists(v => v.InternalName == request.InternalName))
            {
                throw new NotImplementedException($"internal name {request.InternalName} is already taken");
            }

            if (request.Provider == "aws")
            {
                var config = new AwsVoiceConfig(request.SpecialStyles!, request.Engines!);
                newVoice = new Voice(request.DisplayName, request.InternalName, request.Locale, request.Gender, VoiceProvider.CreateAwsProvider(), VoiceType.CreateFromValue(request.VoiceTypeValue), config, null, null);
            }
            else if(request.Provider == "google")
            {
                var config = new GoogleVoiceConfig(request.LanguageCodes!, (int) request.NaturalSampleRateHertz!);
                newVoice = new Voice(request.DisplayName, request.InternalName, request.Locale, request.Gender, VoiceProvider.CreateGoogleProvider(), VoiceType.CreateFromValue(request.VoiceTypeValue), null, config, null);
            }
            else if (request.Provider == "azure")
            {
                var config = new AzureVoiceConfig((int) request.SampleRateHerz!, request.VoiceType!, (int) request.WordsPerMinute!, request.StyleList, request.RoleplayList, request.IsHighQuality48k);
                newVoice = new Voice(request.DisplayName, request.InternalName, request.Locale, request.Gender, VoiceProvider.CreateAzureProvider(), VoiceType.CreateFromValue(request.VoiceTypeValue), null, null, config);
            }
            else
            {
                throw new NotImplementedException();
            }

            var voice = await _voiceRepository.AddAsync(newVoice);
            return voice;
        }
    }
}
