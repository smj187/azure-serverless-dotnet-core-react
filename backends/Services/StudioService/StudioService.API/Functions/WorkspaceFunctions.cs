using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using StudioService.Contracts.Requests.Project;
using StudioService.Contracts.Requests.Workspace;
using StudioService.Contracts.Responses.Workspace;
using StudioService.Core.Domain;
using StudioService.Core.Domain.Project;
using StudioService.Core.Domain.Workspace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net.Mime;
using System.Web.Http;
using System.Net;
using StudioService.API.Extensions;

namespace StudioService.API.Functions
{
    
    public class WorkspaceFunctions
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public WorkspaceFunctions(IWorkspaceRepository workspaceRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _workspaceRepository = workspaceRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [FunctionName("v1-workspaces-create")]
        public async Task<IActionResult> CreateWorkspaceAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/workspaces/create")] HttpRequest req)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<CreateWorkspaceRequest>();
                var userId = req.GetUserIdFromHeaders();

                var workspace = new Workspace(userId, body.Name, WorkspaceType.ScriptWorkspace);
                var created = await _workspaceRepository.AddAsync(workspace);

                var mapped = _mapper.Map<WorkspaceResponse>(created, opts =>
                {
                    opts.Items["projects"] = new List<Project>();
                });
                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }

        

        [FunctionName("v1-workspaces-list")]
        public async Task<IActionResult> ListWorkspacesAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/workspaces/list")] HttpRequest req)
        {
            try
            {
                var userId = req.GetUserIdFromHeaders();

                var workspaces = await _workspaceRepository.ListAsync(x => x.UserId == userId);


                return new OkObjectResult(_mapper.Map<List<WorkspaceMetaDataResponse>>(workspaces));
            } 
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }


        [FunctionName("v1-workspaces-find")]
        public async Task<IActionResult> FindWorkspace(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/workspaces/{workspaceid:guid}/find")] HttpRequest req,
            Guid workspaceId)
        {
            try
            {
                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                var projects = await _projectRepository.ListAsync(workspace.FindAllProjectIds());

                var mapped = _mapper.Map<WorkspaceResponse>(workspace, opts =>
                {
                    opts.Items["projects"] = projects;
                });

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }


        [FunctionName("v1-workspaces-patch-description")]
        public async Task<IActionResult> PatchWorkspaceDescriptionAsync(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/workspaces/{workspaceid:guid}/description")] HttpRequest req,
            Guid workspaceId)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<PatchWorkspaceNameRequest>();

                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }
                workspace.PatchName(body.Name);
                await _workspaceRepository.PatchAsync(workspace);


                var mapped = _mapper.Map<PatchWorkspaceNameResponse>(workspace);
                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }


        [FunctionName("v1-workspaces-delete")]
        public async Task<IActionResult> DeleteWorkspaceAsync(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "v1/workspaces/{workspaceid:guid}/delete")] HttpRequest req,
           Guid workspaceId)
        {
            try
            {
                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                var projects = workspace.FindAllProjectIds();
                await _projectRepository.DeleteManyAsync(projects);
                await _workspaceRepository.DeleteAsync(workspaceId);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }






        [FunctionName("v1-workspaces-folders-create")]
        public async Task<IActionResult> CreateWorkspaceFolderAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/workspaces/{workspaceid:guid}/folders/{folderid:guid}/create")] HttpRequest req,
            Guid workspaceId,
            Guid folderId)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<CreateFolderRequest>();

                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                var newFolder = new Folder(body.Name, body.Description);

                // insert into workspace
                if (workspaceId == folderId)
                {
                    workspace.Subfolders.Add(newFolder);
                }
                // insert folder into parent folder
                else
                {
                    var parentFolderId = folderId;

                    var parentFolder = workspace.FindWorkspaceSubfolder(parentFolderId);
                    if (parentFolder == null)
                    {
                        return new NotFoundResult();
                    }

                    parentFolder.Subfolders.Add(newFolder);
                }

                await _workspaceRepository.PatchAsync(workspace);

                var mapped = _mapper.Map<FolderResponse>(newFolder, opts =>
                {
                    opts.Items["parentFolderId"] = workspaceId == folderId ? null : folderId;
                });

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }

        [FunctionName("v1-workspaces-folders-find")]
        public async Task<IActionResult> FindFolderDescriptionAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/workspaces/{workspaceid:guid}/folders/{folderid:guid}")] HttpRequest req,
            Guid workspaceId,
            Guid folderId)
        {
            try
            {
                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                var folder = workspace.FindWorkspaceSubfolder(folderId);
                if (folder == null)
                {
                    return new NotFoundResult();
                }

                var projects = await _projectRepository.ListAsync(folder.Projects);

                var mapped = _mapper.Map<FindFolderResponse>(folder, opts =>
                {
                    opts.Items["parentFolderId"] = workspaceId == folderId ? null : folderId;
                    opts.Items["projects"] = projects.ToList();
                });

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }

        [FunctionName("v1-workspaces-folders-patch-description")]
        public async Task<IActionResult> PatchWorkspaceFolderDescriptionAsync(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/workspaces/{workspaceid:guid}/folders/{folderid:guid}/description")] HttpRequest req,
            Guid workspaceId,
            Guid folderId)
        {
            try
            {
                var body = await req.GetJsonBodyFromRequest<PatchFolderDescriptionRequest>();
                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                var folder = workspace.FindWorkspaceSubfolder(folderId);
                if (folder == null)
                {
                    return new NotFoundResult();
                }

                folder.PatchDescription(body.Name, body.Description);
                await _workspaceRepository.PatchAsync(workspace);

                var mapped = _mapper.Map<FolderResponse>(folder, opts =>
                {
                    opts.Items["parentFolderId"] = workspaceId == folderId ? null : folderId;
                });

                return new OkObjectResult(mapped);
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }


        [FunctionName("v1-workspaces-folders-delete")]
        public async Task<IActionResult> DeleteWorkspaceFolderAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "v1/workspaces/{workspaceid:guid}/folders/{folderid:guid}/delete")] HttpRequest req,
            Guid workspaceId,
            Guid folderId)
        {
            try
            {
                var workspace = await _workspaceRepository.FindAsync(workspaceId);
                if (workspace == null)
                {
                    return new NotFoundResult();
                }

                var projectsToDelete = workspace.DeleteFolderContent(folderId);

                await _projectRepository.DeleteManyAsync(projectsToDelete);
                await _workspaceRepository.PatchAsync(workspace);

      
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }

    }
}
