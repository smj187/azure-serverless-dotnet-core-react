using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Application.Commands;
using WorkspaceService.Application.Queries;
using WorkspaceService.Contracts.Requests;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{projectid:guid}")]
        public async Task<IActionResult> FindProjectAsync([FromRoute] Guid projectId)
        {
            var query = new FindProjectQuery
            {
                ProjectId = projectId
            };

            var data = await _mediator.Send(query);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
        
        [HttpPatch]
        [Route("{projectid:guid}/name")]
        public async Task<IActionResult> PatchProjectNameAsync([FromRoute] Guid projectId, [FromBody] PatchProjectNameRequest request)
        {
            var command = new PatchProjectNameCommand
            {
                ProjectId = projectId,
                Name = request.Name
            };

            var data = await _mediator.Send(command);
       
            return Ok(data);
        }

        [HttpPatch]
        [Route("{projectid:guid}/image")]
        public async Task<IActionResult> PatchProjectImageAsync([FromRoute] Guid projectId, [FromForm] PatchProjectImageRequest request)
        {
            var command = new PatchProjectImageCommand
            {
                ProjectId = projectId,
                Image = request.Image
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }

        [HttpDelete]
        [Route("{projectid:guid}")]
        public async Task<IActionResult> DeleteProjectAsync([FromRoute] Guid projectId)
        {
            var command = new DeleteProjectCommand
            {
                ProjectId = projectId,
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }


        [HttpPost]
        [Route("create/script")]
        public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectRequest request)
        {
            var command = new CreateProjectCommand
            {
                Name = request.Name,
                Description = request.Description,
                ProjectType = ProjectType.CreateScriptProjectType()
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }




        [HttpPatch]
        [Route("{projectid:guid}/audioblocks/add")]
        public async Task<IActionResult> AddAudioBlockToProjectAsync([FromRoute] Guid projectId, [FromBody] AddAudioBlockToProjectRequest request)
        {
            var command = new AddAudioBlockCommand
            {
                Index = request.Index,
                ProjectId = projectId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }
        
        [HttpPatch]
        [Route("{projectid:guid}/audioblocks/value")]
        public async Task<IActionResult> PatchAudioBlockValueAsync([FromRoute] Guid projectId, [FromBody] PatchAudioBlockValueRequest request)
        {
            var command = new PatchAudioBlockValueCommand
            {
                ProjectId = projectId,
                AudioBlockId = request.AudioBlockId,
                Value = request.Value
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }
        
        [HttpPatch]
        [Route("{projectid:guid}/audioblocks/voice")]
        public async Task<IActionResult> PatchAudioBlockValueAsync([FromRoute] Guid projectId, [FromBody] PatchAudioBlockVoiceRequest request)
        {
            var command = new PatchAudioBlockVoiceCommand
            {
                ProjectId = projectId,
                AudioBlockId = request.AudioBlockId,
                VoiceId = request.VoiceId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }
                
        [HttpPatch]
        [Route("{projectid:guid}/audioblocks/index")]
        public async Task<IActionResult> PatchAudioBlockIndexAsync([FromRoute] Guid projectId, [FromBody] PatchAudioBlockIndexRequest request)
        {
            var command = new PatchAudioBlockIndexCommand
            {
                ProjectId = projectId,
                AudioBlockId = request.AudioBlockId,
                Index = request.Index
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }

        [HttpPatch]
        [Route("{projectid:guid}/audioblocks/remove")]
        public async Task<IActionResult> RemoveAudioBlockFromProjectAsync([FromRoute] Guid projectId, [FromBody] RemoveAudioBlockFromProjectRequest request)
        {
            var command = new RemoveAudioBlockCommand
            {
                AudioBlockId = request.AudioBlockId,
                ProjectId = projectId
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }
    }
}
