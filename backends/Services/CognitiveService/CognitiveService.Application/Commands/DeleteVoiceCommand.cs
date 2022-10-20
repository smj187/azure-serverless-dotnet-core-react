using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Commands
{
    public class DeleteVoiceCommand : IRequest
    {
        public Guid VoiceId { get; set; }
    }
}
