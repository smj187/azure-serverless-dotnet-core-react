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
    public class AssignParentFolderIdMappingAction : IMappingAction<Folder, FolderResponse>
    {
        public void Process(Folder source, FolderResponse destination, ResolutionContext context)
        {
            var parentFolderId = context.Items["parentFolderId"];
            if (parentFolderId != null)
            {
                var id = Guid.Parse(parentFolderId.ToString());
                destination.ParentFolderId = id;
            }
        }
    }
}
