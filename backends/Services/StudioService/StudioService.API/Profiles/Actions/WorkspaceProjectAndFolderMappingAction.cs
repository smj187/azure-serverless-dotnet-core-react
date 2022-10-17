using AutoMapper;
using StudioService.Contracts.Responses.Workspace;
using StudioService.Core.Domain.Project;
using StudioService.Core.Domain.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.API.Profiles.Actions
{
    public class WorkspaceProjectAndFolderMappingAction : IMappingAction<Workspace, WorkspaceResponse>
    {
        private static List<WorkspaceProjectResponse> MapProjects(ResolutionContext context, List<Project> projects, List<Guid> projectIds)
        {
            // convert database project to workspace project response
            var mappedProjects = new List<WorkspaceProjectResponse>();

            foreach (var projectId in projectIds)
            {
                var project = projects.FirstOrDefault(p => p.Id == projectId);
                if (project != null)
                {
                    projects.Remove(project);
                    var mapped = context.Mapper.Map<WorkspaceProjectResponse>(project);
                    mappedProjects.Add(mapped);
                }
            }

            return mappedProjects;
        }

        private void MapSubfolders(ResolutionContext context, WorkspaceResponse destination, List<Project> projects, Folder folder, Guid? parentFolderId = null)
        {
            // map folder
            var mapped = context.Mapper.Map<WorkspaceFolderResponse>(folder);
            mapped.ParentFolderId = parentFolderId;
            mapped.Projects.AddRange(MapProjects(context, projects, folder.Projects));

            destination.Folders.Add(mapped);

            // check if we need to map subfolders
            foreach (var subfolder in folder.Subfolders)
            {
                MapSubfolders(context, destination, projects, subfolder, folder.Id);
            }
        }



        public void Process(Workspace source, WorkspaceResponse destination, ResolutionContext context)
        {
            // projects from the database
            var projects = (List<Project>)context.Items["projects"];

            // add mapped projects to as workspace projects
            destination.Projects.AddRange(MapProjects(context, projects, source.Projects));

            // map subfolder tree to list
            foreach (var subfolder in source.Subfolders)
            {
                MapSubfolders(context, destination, projects, subfolder, null);
            }

            destination.ProjectsCount = source.FindAllProjectIds().Count;
        }
    }
}
