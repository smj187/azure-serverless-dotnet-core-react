using Amazon.Runtime.Internal;
using CognitiveService.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Commands
{
    public class PatchVoicePreviewCommand : IRequest<Voice>
    {
        public Guid VoiceId { get; set; }
        public IFormFile? Audio { get; set; } = null;
    }
}
