using AutoMapper;
using BuildingBlocks.Extensions.Strings;
using CognitiveService.Application.Commands;
using CognitiveService.Application.Queries;
using CognitiveService.Contracts.Requests;
using CognitiveService.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.ComponentModel.DataAnnotations;

namespace CognitiveService.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VoicesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public VoicesController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> VoicesAsync()
        {
            var query = new ListVoicesQuery();

            var data = await _mediator.Send(query);

            return Ok(_mapper.Map<IReadOnlyCollection<UserVoicesResponse>>(data));
        }

        [HttpGet]
        [Route("{voiceId:guid}")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> FindVoiceAsync([FromRoute] Guid voiceId)
        {
            var query = new FindVoiceQuery
            {
                VoiceId = voiceId
            };

            var data = await _mediator.Send(query);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet]
        [Route("list")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> ListVoicesAsync()
        {
            return Ok(await _mediator.Send(new ListVoicesQuery()));
        }

        [HttpPost]
        [Route("create")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> CreateVoiceAsync([Required, FromBody] CreateVoiceRequest request)
        {
            var command = new CreateVoiceCommand
            {
                DisplayName = request.DisplayName,
                InternalName = request.InternalName,
                Gender = request.Gender,
                Locale = request.Locale,
                Provider = request.Provider,
                VoiceTypeValue = request.VoiceType,

                // aws
                Engines = request.AwsConfig?.Engines,
                SpecialStyles = request.AwsConfig?.SpecialStyles,

                //google
                NaturalSampleRateHertz = request.GoogleConfig?.NaturalSampleRateHertz,
                LanguageCodes = request.GoogleConfig?.LanguageCodes,

                // azure
                SampleRateHerz = request.AzureConfig?.SampleRateHertz,
                VoiceType = request.AzureConfig?.VoiceType,
                WordsPerMinute = request.AzureConfig?.WordsPerMinute,
                StyleList = request.AzureConfig?.StyleList,
                RoleplayList = request.AzureConfig?.RolePlayList,
                IsHighQuality48k = request.AzureConfig?.IsHighQuality48K
            };

            var data = await _mediator.Send(command);


            return Ok(data);
        }

        

        [HttpPatch]
        [Route("name")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> PatchVoiceNameAsync([FromBody] PatchVoiceNameRequest request)
        {
            var command = new PatchVoiceNameCommand
            {
                NewDisplayName = request.Name,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }        

        [HttpPatch]
        [Route("avatar")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> PatchVoiceAvatarAsync([FromForm] PatchVoiceAvatarRequest request)
        {
            var command = new PatchVoiceAvatarCommand
            {
                Image = request.Image,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }

        [HttpPatch]
        [Route("preview")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> PatchPreviewVoiceAsync([FromForm] PatchVoicePreviewRequest request)
        {
            var command = new PatchVoicePreviewCommand
            {
                Audio = request.Audio,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }

        [HttpPatch]
        [Route("available")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> PatchAvailablityAsync([FromBody] PatchVoiceAvailablityRequest request)
        {
            var command = new PatchVoiceAvailabilityCommand
            {
                IsAvailable = request.IsAvailable,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }
        
        [HttpPatch]
        [Route("type")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> PatchTypeAsync([FromBody] PatchVoiceTypeRequest request)
        {
            var command = new PatchVoiceTypeCommand
            {
                VoiceType = request.VoiceType,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }


        [HttpDelete]
        [Route("{voiceId}")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> DeleteVoiceAsync([FromRoute] Guid voiceId)
        {
            var command = new DeleteVoiceCommand
            {
                VoiceId = voiceId
            };

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [Route("discover/aws")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> DiscoverAwsVoicesAsync()
        {
            var query = new FindAwsVoicesQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet]
        [Route("discover/google")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> DiscoverGoogleVoicesAsync()
        {
            var query = new FindGoogleVoicesQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet]
        [Route("discover/azure")]
        [RequiredScope(new string[] { "admin-privileges" })]
        public async Task<IActionResult> DiscoverAzureVoicesAsync()
        {
            var query = new FindAzureVoicesQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }
    }
}
