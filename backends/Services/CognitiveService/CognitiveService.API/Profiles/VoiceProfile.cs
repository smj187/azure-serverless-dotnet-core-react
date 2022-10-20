using AutoMapper;
using CognitiveService.API.Profiles.Actions;
using CognitiveService.Contracts.Requests;
using CognitiveService.Contracts.Responses;
using CognitiveService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.API.Profiles
{
    public class VoiceProfile : Profile
    {
        public VoiceProfile()
        {
            CreateMap<Voice, UserVoicesResponse>(MemberList.Destination)
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.DisplayName))
                .ForMember(dest => dest.AvatarUrl, opts => opts.MapFrom(s => s.AvatarImage.Url))
                .ForMember(dest => dest.AvatarContentType, opts => opts.MapFrom(s => s.AvatarImage.ContentType))
                .ForMember(dest => dest.PreviewUrl, opts => opts.MapFrom(s => s.PreviewAudio.Url))
                .ForMember(dest => dest.PreviewContentType, opts => opts.MapFrom(s => s.PreviewAudio.ContentType))
                .AfterMap<SpecialVoiceStyleMapping>();
        }
    }
}
