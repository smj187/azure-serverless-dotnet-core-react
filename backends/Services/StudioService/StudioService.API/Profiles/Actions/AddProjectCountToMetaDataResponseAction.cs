using AutoMapper;
using StudioService.Contracts.Responses.Workspace;
using StudioService.Core.Domain.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.API.Profiles.Actions
{
    public class AddProjectCountToMetaDataResponseAction : IMappingAction<Workspace, WorkspaceMetaDataResponse>
    {
        public void Process(Workspace source, WorkspaceMetaDataResponse destination, ResolutionContext context)
        {
            destination.ProjectsCount = source.FindAllProjectIds().Count;
        }
    }
}
