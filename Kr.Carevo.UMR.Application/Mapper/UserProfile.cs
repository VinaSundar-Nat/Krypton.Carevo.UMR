using AutoMapper;
using Kr.Carevo.UMR.Domain.Models.AggregateModels;

namespace Kr.Carevo.UMR.Application.Mapper;

public class UserProfile : Profile
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

        CreateMap<Employer, EmploymentResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var userEmployer = src.UserEmployers?.FirstOrDefault();
                return userEmployer?.StartDate ?? default;
            }))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var userEmployer = src.UserEmployers?.FirstOrDefault();
                return userEmployer?.EndDate ?? default;
            }))
            .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var userEmployer = src.UserEmployers.FirstOrDefault();
                return userEmployer?.Duration ?? string.Empty;
            }))
            .ForMember(dest => dest.Projects, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var userEmployer = src.UserEmployers?.FirstOrDefault();
                return userEmployer?.Projects ?? [];
            }));

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.EmploymentId, opt => opt.MapFrom(src => src.UserEmployerId))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills));

        CreateMap<Skill, SkillDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.EffectiveDate, opt => opt.Ignore());
    }
}
