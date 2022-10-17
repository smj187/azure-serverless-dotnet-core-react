using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using BuildingBlocks.BlobStorage.Interfaces;
using CognitiveService.Application.Commands;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CognitiveService.API.Controllers
{
    [Route("[controller]")]
    [Route("api/v1/[controller]")]
    public class TTSController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TTSController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
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
