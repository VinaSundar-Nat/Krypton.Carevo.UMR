using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Interface;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public sealed class Employer : BaseEntity<Employer>, IAggregateRoot
{
    public string Company { get; private set; } = string.Empty!;
    public string? Logo { get; private set; }
    public string? Url { get; private set; }
    public ICollection<UserEmployer> UserEmployers { get; private set; } = [];
   

    public void CreateEmployment(EmploymentDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Company))
            throw new ArgumentException("Company cannot be empty.", nameof(dto.Company));

        if (dto.StartDate == default)
            throw new ArgumentException("StartDate must be a valid date.", nameof(dto.StartDate));

        if (dto.EndDate.HasValue && dto.EndDate.Value < dto.StartDate)
            throw new ArgumentException("EndDate cannot be before StartDate.", nameof(dto.EndDate));

            Company = dto.Company;
            Logo = dto.Logo;
            Url = dto.Url;
    }

    public void UpdateEmployment(string company, DateTime startDate, DateTime? endDate, string? logo, string? url)
    {
        if (string.IsNullOrWhiteSpace(company))
            throw new ArgumentException("Company is required.", nameof(company));

        if (startDate == default)
            throw new ArgumentException("StartDate must be a valid date.", nameof(startDate));

        if (endDate.HasValue && endDate.Value < startDate)
            throw new ArgumentException("EndDate cannot be before StartDate.", nameof(endDate));

        Company = company;
        Logo = logo;
        Url = url;
    }



   
}
