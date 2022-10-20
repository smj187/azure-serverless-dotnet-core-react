using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Commands
{
    public class PatchVoiceAvailabilityCommand : IRequest<Voice>
    {
        public Guid VoiceId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
