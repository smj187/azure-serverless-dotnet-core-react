using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Commands
{
    public class SynthesisSpeechCommand : IRequest<string>
    {
        public string? Text { get; set; } = null;
        public string? Ssml { get; set; } = null;
        public Guid VoiceId { get; set; }
        public string Locale { get; set; }
    }
}
