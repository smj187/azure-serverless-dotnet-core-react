using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StudioService.Contracts.Requests.Project;
using StudioService.Core.Domain.Workspace;
using StudioService.Core.Domain.Project;
using System.Collections.Generic;
using StudioService.API.Extensions;
using StudioService.Contracts.Requests.Workspace;
using System.Web.Http;
using AutoMapper;
using StudioService.Contracts.Responses.Project;
using StudioService.Application.Services;

namespace StudioService.API.Functions
{
    public class ProjectFunctions
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IAzureService _azureService;
        private readonly IBlobService _blobService;

        public ProjectFunctions(IMapper mapper, IProjectRepository projectRepository, IWorkspaceRepository workspaceRepository, IAzureService azureService, IBlobService blobService)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _workspaceRepository = workspaceRepository;
            _azureService = azureService;
            _blobService = blobService;
        }

        [FunctionName("v1-projects-create")]
        public async Task<IActionResult> CreateProjectAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/projects/create")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<CreateProjectRequest>();

                // create project
                var project = new Project(body.Name, body.WorkspaceId, body.FolderId, body.Description);
                await _projectRepository.AddAsync(project);


                var workspace = await _workspaceRepository.FindAsync(body.WorkspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                // add workspace project
                if (body.FolderId == null)
                {
                    workspace.AddWorkspaceProject(project.Id);
                }
                // add folder project
                else
                {
                    var folderId = Guid.Parse(body.FolderId.ToString());

                    var folder = workspace.FindWorkspaceSubfolder(folderId);
                    folder.Projects.Add(project.Id);
                }

                await _workspaceRepository.PatchAsync(workspace);

                var mapped = _mapper.Map<ProjectResponse>(project);

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }


        [FunctionName("v1-projects-find")]
        public async Task<IActionResult> FindProjectByIdAsync(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/projects/{projectid:guid}/find")] HttpRequest req,
           Guid projectId,
           ILogger log)
        {
            try
            {
                var project = await _projectRepository.FindAsync(projectId);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                var mapped = _mapper.Map<ProjectResponse>(project);
                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }

        [FunctionName("v1-projects-patch-description")]
        public async Task<IActionResult> PatchProjectDescriptionAsync(
           [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/projects/{projectid:guid}/description")] HttpRequest req,
           Guid projectId,
           ILogger log)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<PatchProjectDescriptionRequest>();

                var project = await _projectRepository.FindAsync(projectId);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                project.PatchDescription(body.Name, body.Description);

                await _projectRepository.PatchAsync(project);

                var mapped = _mapper.Map<ProjectResponse>(project);
                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }

            
        }

        [FunctionName("v1-projects-delete")]
        public async Task<IActionResult> DeleteProjectAsync(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "v1/projects/{projectid:guid}/{workspaceid:guid}/{folderid:guid}")] HttpRequest req,
           Guid projectId,
           Guid workspaceId,
           Guid folderId,
           ILogger log)
        {
            try
            {
                var project = await _projectRepository.FindAsync(projectId);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                // workspace project deletion
                if (workspaceId == folderId)
                {
                    workspace.RemoveWorkspaceProject(projectId);
                }
                // folder project
                else
                {
                    var folder = workspace.FindWorkspaceSubfolder(folderId);
                    if (folder == null)
                    {
                        throw new NotImplementedException();
                    }
                    folder.DeleteProject(projectId);
                }



                // delete project itself
                await _workspaceRepository.PatchAsync(workspace);
                await _projectRepository.DeleteAsync(projectId);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
            
        }



        [FunctionName("v1-projects-content-add")]
        public async Task<IActionResult> AddAudioContentAsync(
           [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/projects/{projectid:guid}/content/add")] HttpRequest req,
           Guid projectId,
           ILogger log)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<AddContentRequest>();

                var project = await _projectRepository.FindAsync(projectId);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                var content = project.AddContent(body.Index);

                await _projectRepository.PatchAsync(project);

                var mapped = _mapper.Map<AudioContentResponse>(content);

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }

        [FunctionName("v1-projects-content-remove")]
        public async Task<IActionResult> RemoveAudioContentAsync(
           [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/projects/{projectid:guid}/content/remove")] HttpRequest req,
           Guid projectId,
           ILogger log)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<RemoveContentRequest>();

                var project = await _projectRepository.FindAsync(projectId);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                var content = project.RemoveContent(body.ContentId);

                await _projectRepository.PatchAsync(project);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }

            
        }
        
        
        [FunctionName("v1-projects-content-patch-value")]
        public async Task<IActionResult> PatchContentValueAsync(
           [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/projects/{projectid:guid}/content/value")] HttpRequest req,
           Guid projectId,
           ILogger log)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<PatchAudioContentRequest>();

                var project = await _projectRepository.FindAsync(projectId);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                var content = project.PatchContentValue(body.ContentId, body.Value);

                await _projectRepository.PatchAsync(project);

                var mapped = _mapper.Map<AudioContentResponse>(content);

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);

            }
        }


        [FunctionName("v1-projects-content-reorder")]
        public async Task<IActionResult> ReorderAudioContentAsync(
           [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/projects/{projectid:guid}/content/reorder")] HttpRequest req,
           Guid projectId,
           ILogger log)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<OrderContentRequest>();

                var project = await _projectRepository.FindAsync(projectId);
                if (project == null)
                {
                    return new NotFoundResult();
                }

                var contents = project.OrderContent(body.ContentId, body.NewIndex);

                await _projectRepository.PatchAsync(project);

                var mapped = _mapper.Map<List<ReorderedAudioContentResponse>>(contents);

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);

            }
        }


        [FunctionName("v1-projects-content-generate")]
        public async Task<IActionResult> GenerateAudioContentAsync(
           [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/projects/{projectid:guid}/content/generate")] HttpRequest req,
           Guid projectId,
           ILogger log)
        {
            var request = await new StreamReader(req.Body).ReadToEndAsync();
            var body = JsonConvert.DeserializeObject<GenerateAudioRequest>(request);

            var project = await _projectRepository.FindAsync(projectId);
            if (project == null)
            {
                throw new NotImplementedException();
                //return new NotFoundResult();
            }

            // TODO:
            var content = project.GenerateAudio(body.ContentId);

            var audio = await _azureService.GernateAudio(content.Value);

            //await _projectRepository.PatchAsync(project);

            //var mapped = _mapper.Map<AudioContentResponse>(content);
            //return new OkObjectResult(mapped);
            return new OkObjectResult(audio);
        }
    }
}
