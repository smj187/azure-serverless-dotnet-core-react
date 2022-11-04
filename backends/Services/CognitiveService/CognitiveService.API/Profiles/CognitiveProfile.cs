using AutoMapper;
using CognitiveService.Application.DTOs;
using CognitiveService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.API.Profiles
{
    public class CognitiveProfile : Profile
    {
        public CognitiveProfile()
        {
            CreateMap<KeyPhraseExtractionDocumentRequest, KeyPhraseExtractionDocumentRequestDTO>(MemberList.Destination)
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Id.ToString()));
            
            CreateMap<LanguageDetectionDocumentRequest, LanguageDetectionDocumentRequestDTO>(MemberList.Destination)
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Id.ToString()));            
            
            CreateMap<EntityRecognitionDocumentRequest, EntityRecognitionDocumentRequestDTO>(MemberList.Destination)
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Id.ToString()));
                            
            CreateMap<TranslationValueRequest, TranslationRequestDTO>(MemberList.Destination)
                .ForMember(dest => dest.Text, opts => opts.MapFrom(s => s.Text.ToString()));
                
        }
    }
}
