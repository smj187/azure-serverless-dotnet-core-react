using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Commands
{
    public class CreateVoiceCommand : IRequest<Voice>
    {
        public string Provider { get; set; } = default!;

        public string DisplayName { get; set; } = default!;
        public string InternalName { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Locale { get; set; } = default!;
        public int VoiceTypeValue { get; set; }

        // aws
        public List<string>? SpecialStyles { get; set; } = new();
        public List<string>? Engines { get; set; } = new();

        // google
        public List<string>? LanguageCodes { get; set; } = new();
        public int? NaturalSampleRateHertz { get; set; }

        // azure
        public int? SampleRateHerz { get; set; }
        public string? VoiceType { get; set; }
        public int? WordsPerMinute { get; set; }
        public List<string>? StyleList { get; set; }
        public List<string>? RoleplayList { get; set; }
        public bool? IsHighQuality48k { get; set; }

    }
}
