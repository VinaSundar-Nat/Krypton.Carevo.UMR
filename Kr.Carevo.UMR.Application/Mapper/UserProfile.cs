using System;
using AutoMapper;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Models.AggregateModels;

namespace Kr.Carevo.UMR.Application.Mapper;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contacts))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Code)));

        CreateMap<ICollection<Contact>, ContactDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => 
                src.Where(c => c.Type == ContactType.Email && !string.IsNullOrEmpty(c.Value))
                   .Select(c => c.Value)
                   .FirstOrDefault() ?? string.Empty))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => 
                src.Where(c => c.Type == ContactType.Phone && !string.IsNullOrEmpty(c.Value))
                   .Select(c => c.Value)
                   .FirstOrDefault() ?? string.Empty))
            .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => 
                src.Where(c => c.Type == ContactType.Mobile && !string.IsNullOrEmpty(c.Value))
                   .Select(c => c.Value)
                   .FirstOrDefault() ?? string.Empty));
    }
}
