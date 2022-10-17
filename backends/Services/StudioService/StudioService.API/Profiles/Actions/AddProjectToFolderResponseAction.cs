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
    public class AddProjectToFolderResponseAction : IMappingAction<Folder, FindFolderResponse>
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

        public void Process(Folder source, FindFolderResponse destination, ResolutionContext context)
        {
            // projects from the database
            var projects = (List<Project>)context.Items["projects"];

            // add mapped projects to as workspace projects
            destination.Projects.AddRange(MapProjects(context, projects, source.Projects));
        }
    }
}
