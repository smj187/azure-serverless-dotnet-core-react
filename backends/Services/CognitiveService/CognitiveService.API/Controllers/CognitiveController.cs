using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using AutoMapper;
using BuildingBlocks.BlobStorage.Interfaces;
using CognitiveService.Application.Commands;
using CognitiveService.Application.DTOs;
using CognitiveService.Application.Queries;
using CognitiveService.Application.Services.Models;
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
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CognitiveController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CognitiveController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("speech-synthesis")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> TTS([FromBody] CreateTTSRequest request)
        {
            if (request.Text == null && request.Ssml == null)
            {
                throw new NotImplementedException("at least one must be set");
            }

            var command = new SynthesisSpeechCommand
            {
                Locale = request.Locale,
                Text = request.Text,
                Ssml = request.Ssml,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }

        [HttpPost]
        [Route("find-key-phrase")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> ExtractKeyPhrase([FromBody] KeyPhraseExtractionRequest request)
        {
            var query = new FindKeyPhrasesQuery
            {
                Documents = _mapper.Map<IEnumerable<KeyPhraseExtractionDocumentRequestDTO>>(request.Documents).ToList()
            };
     
            var data = await _mediator.Send(query);
            return Ok(data);
        }
        
        [HttpPost]
        [Route("detect-language")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> LanguageDetection([FromBody] LanguageDetectionRequest request)
        {
            var command = new DetectLanguageQuery
            {
                Documents = _mapper.Map<IEnumerable<LanguageDetectionDocumentRequestDTO>>(request.Documents).ToList()
            };
     
            var data = await _mediator.Send(command);
            return Ok(data);
        }
                
        [HttpPost]
        [Route("find-entities")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> EntityRecognition([FromBody] EntityRecognitionRequest request)
        {
            var command = new EntityRecognitionQuery
            {
                Documents = _mapper.Map<IEnumerable<EntityRecognitionDocumentRequestDTO>>(request.Documents).ToList()
            };
     
            var data = await _mediator.Send(command);
            return Ok(data);
        }
                        
        [HttpPost]
        [Route("find-translations")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> Translation([FromBody] TranslationRequest request)
        {
            var command = new FindTranslationsQuery
            {
                Translations = _mapper.Map<IEnumerable<TranslationRequestDTO>>(request.Translations).ToList(),
                Locale = request.Locale
            };
     
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        
        [HttpPost]
        [Route("find-remote-image-tags")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> GetImageTags([FromBody] ExtractImageTagsFromRemoteRequest request)
        {
            var command = new FindImageTagsQuery
            {
                Url = request.Url,
            };
     
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        
        [HttpPost]
        [Route("find-source-image-tags")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> GetImageTags([FromForm] ExtractImageTagsFromImageRequest request)
        {
            var command = new FindImageTagsQuery
            {
                Image = request.Image
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }
                
        [HttpPost]
        [Route("find-remote-image-captions")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> GetCaptions([FromBody] ExtractCaptionFromRemoteRequest request)
        {
            var command = new FindImageCaptionsQuery
            {
                Url = request.Url,
            };
     
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        
        [HttpPost]
        [Route("find-source-image-captions")]
        [RequiredScope(new string[] { "customer-privileges" })]
        public async Task<IActionResult> GetCaptions([FromForm] ExtractCaptionsFromImageRequest request)
        {
            var command = new FindImageCaptionsQuery
            {
                Image = request.Image
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }

    }
}
