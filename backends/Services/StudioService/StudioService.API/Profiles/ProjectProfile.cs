using AutoMapper;
using StudioService.Contracts.Responses.Project;
using StudioService.Core.Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.API.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {

            CreateMap<AudioContent, ReorderedAudioContentResponse>();

            CreateMap<AudioContent, AudioContentResponse>();

            CreateMap<Project, ProjectResponse>()
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ModifiedAt ?? s.CreatedAt));
        }
    }
}
