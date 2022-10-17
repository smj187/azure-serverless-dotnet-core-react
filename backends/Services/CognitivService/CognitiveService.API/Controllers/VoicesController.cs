using AutoMapper;
using BuildingBlocks.Extensions.Strings;
using CognitiveService.Application.Commands;
using CognitiveService.Application.Queries;
using CognitiveService.Application.Services;
using CognitiveService.Contracts.Requests;
using CognitiveService.Core.Domain;
using EasyCaching.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VoicesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRedisCachingProvider _cache;
        private readonly string _cacheKey = nameof(ListVoicesAsync).ToSnakeCase();

        public VoicesController(IMapper mapper, IMediator mediator, IEasyCachingProviderFactory factory, IConfiguration configuration)
        {
            _mapper = mapper;
            _mediator = mediator;
            _cache = factory.GetRedisProvider(configuration.GetValue<string>("Cache:Database"));
        }

        [HttpGet]
        [Route("list-voices")]
        [RequiredScope()]
        public async Task<IActionResult> ListVoicesAsync()
        {
            //var cache = _cache.StringGet(_cacheKey);

            //IReadOnlyCollection<Voice> voices;

            //if (cache != null && cache.Length > 0)
            //{
            //    voices = JsonConvert.DeserializeObject<IReadOnlyCollection<Voice>>(cache);
            //}
            //else
            //{
            //    var query = new ListVoicesQuery();
            //    voices = await _mediator.Send(query);
            //    _cache.StringSet(_cacheKey, JsonConvert.SerializeObject(voices), TimeSpan.FromSeconds(60 * 60 * 24));
            //}

            //return Ok(voices);

            return Ok(await _mediator.Send(new ListVoicesQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoiceAsync([Required, FromBody] CreateVoiceRequest request)
        {
            var command = new CreateVoiceCommand
            {
                DisplayName = request.DisplayName,
                InternalName = request.InternalName,
                Gender = request.Gender,
                Locale = request.Locale,
                Provider = request.Provider,
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

            _cache.KeyDel(_cacheKey);

            return Ok(data);
        }

        

        [HttpGet]
        [Route("find/{voiceId:guid}")]
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

        [HttpPatch]
        [Route("{voiceid:guid}/display")]
        public async Task<IActionResult> PatchDisplayNameAsync([FromRoute] Guid voiceId, [FromBody] PatchVoiceDisplayRequest request)
        {
            var command = new PatchDisplayNameCommand
            {
                NewDisplayName = request.DisplayName,
                VoiceId = voiceId
            };

            var data = await _mediator.Send(command);
            _cache.KeyDel(_cacheKey);

            return Ok(data);
        }

        [HttpPatch]
        [Route("{voiceid:guid}/availability")]
        public async Task<IActionResult> PatchAvailablityAsync([FromRoute] Guid voiceId, [FromBody] PatchVoiceAvailablityRequest request)
        {
            var command = new PatchAvailabilityCommand
            {
                IsAvailable = request.IsAvailable,
                VoiceId = voiceId
            };

            var data = await _mediator.Send(command);
            _cache.KeyDel(_cacheKey);

            return Ok(data);
        }

        [HttpDelete]
        [Route("{voiceId}")]
        public async Task<IActionResult> DeleteVoiceAsync([FromRoute] Guid voiceId)
        {
            var command = new DeleteVoiceCommand
            {
                VoiceId = voiceId
            };

            await _mediator.Send(command);
            _cache.KeyDel(_cacheKey);
            return NoContent();
        }

        [HttpGet]
        [Route("aws/discover")]
        public async Task<IActionResult> DiscoverAwsVoicesAsync()
        {
            var query = new FindAwsVoicesQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet]
        [Route("google/discover")]
        public async Task<IActionResult> DiscoverGoogleVoicesAsync()
        {
            var query = new FindGoogleVoicesQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet]
        [Route("azure/discover")]
        public async Task<IActionResult> DiscoverAzureVoicesAsync()
        {
            var query = new FindAzureVoicesQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }
    }
}
