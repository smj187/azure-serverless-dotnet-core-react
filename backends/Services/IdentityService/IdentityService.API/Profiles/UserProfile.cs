using AutoMapper;
using IdentityService.Contracts.Responses;
using IdentityService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entry, CreditEntryResponse>(MemberList.Destination)
                .ForMember(dest => dest.Value, opts => opts.MapFrom(s => s.Value))
                .ForMember(dest => dest.Time, opts => opts.MapFrom(s => s.Time))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(s => s.Type))
                .ForMember(dest => dest.Credits, opts => opts.MapFrom(s => s.Credits));

            CreateMap<Credit, CreditResponse>(MemberList.Destination)
                .ForMember(dest => dest.Date, opts => opts.MapFrom(s => s.Date))
                .ForMember(dest => dest.Usage, opts => opts.MapFrom(s => s.GetUsage()))
                .ForMember(dest => dest.Credits, opts => opts.MapFrom(s => s.Entries));

            CreateMap<User, UserProfileResponse>(MemberList.Destination)
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(s => s.B2cObjectId))
                .ForMember(dest => dest.AccountTier, opts => opts.MapFrom(s => s.AccountTier))
                .ForMember(dest => dest.CreditHistoryStartDate, opts => opts.MapFrom(s => s.HistoryStartDate))
                .ForMember(dest => dest.CreditHistoryResetDate, opts => opts.MapFrom(s => s.HistoryResetDate))
                .ForMember(dest => dest.CreditHistory, opts => opts.MapFrom(s => s.CreditHistory))
                .ForMember(dest => dest.TotalAvailableCredits, opts => opts.MapFrom(s => s.TotalAvailableCredits))
                .ForMember(dest => dest.RemainingAvailableCredits, opts => opts.MapFrom(s => s.RemainingAvailableCredits));
        }
    }
}
