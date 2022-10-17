using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Commands
{
    public class PatchDisplayNameCommand : IRequest<Voice>
    {
        public Guid VoiceId { get; set; }
        public string NewDisplayName { get; set; } = default!;
    }
}
