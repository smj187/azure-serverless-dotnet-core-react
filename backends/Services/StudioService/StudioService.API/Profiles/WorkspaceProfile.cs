using AutoMapper;
using StudioService.API.Profiles.Actions;
using StudioService.Contracts.Responses.Workspace;
using StudioService.Core.Domain.Project;
using StudioService.Core.Domain.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.API.Profiles
{
    public class WorkspaceProfile : Profile
    {
        public WorkspaceProfile()
        {
            CreateMap<Folder, FindFolderSubfolderResponse>()
                .ForMember(dest => dest.ProjectCount, opts => opts.MapFrom(s => s.Projects.Count))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt));

            CreateMap<Folder, FindFolderResponse>()
                .ForMember(dest => dest.Projects, opts => opts.Ignore())
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt))
                .AfterMap<AddProjectToFolderResponseAction>();



            // list workspace response
            CreateMap<Workspace, WorkspaceMetaDataResponse>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(s => s.WorkspaceType.Value))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt))
                .AfterMap<AddProjectCountToMetaDataResponseAction>();

            CreateMap<Workspace, WorkspaceResponse>()
                .ForMember(dest => dest.Projects, opts => opts.Ignore())
                .ForMember(dest => dest.Folders, opts => opts.Ignore())
                .ForMember(dest => dest.ProjectsCount, opts => opts.MapFrom(s => s.Projects.Count))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(s => s.WorkspaceType.Value))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt))
                .AfterMap<WorkspaceProjectAndFolderMappingAction>();



            CreateMap<Project, WorkspaceProjectResponse>()
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt));


            CreateMap<Folder, WorkspaceFolderResponse>()
                .ForMember(dest => dest.Projects, opts => opts.Ignore())
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt));


         

            CreateMap<Workspace, PatchWorkspaceNameResponse>();



            CreateMap<Folder, FolderResponse>()
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt))
                .AfterMap<AssignParentFolderIdMappingAction>();
        }
    }
}
