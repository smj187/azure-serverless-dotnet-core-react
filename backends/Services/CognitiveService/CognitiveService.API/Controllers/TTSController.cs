using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using BuildingBlocks.BlobStorage.Interfaces;
using CognitiveService.Application.Commands;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CognitiveService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CognitiveController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CognitiveController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("speech-synthesis")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> TTS([FromBody] CreateTTSRequest request)
        {
            var command = new SynthesisSpeechCommand
            {
                Locale = request.Locale,
                Value = request.Value,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }
    }
}
