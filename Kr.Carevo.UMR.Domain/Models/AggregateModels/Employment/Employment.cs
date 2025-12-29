using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Interface;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public class Employment : BaseEntity<Employment>, IAggregateRoot
{
    public string Company { get; private set; } = string.Empty!;
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string? Logo { get; private set; }
    public string? Url { get; private set; }
    public int UserId { get; private set; }
    public User? User { get; set; }
    public ICollection<Project> Projects { get; private set; } = [];

    public string Duration
    {
        get
        {
            var end = EndDate ?? DateTime.UtcNow;
            var duration = end - StartDate;
            var years = duration.Days / 365;
            var months = (duration.Days % 365) / 30;
            var days = (duration.Days % 365) % 30;

            var parts = new List<string>();
            if (years > 0) parts.Add($"{years} year{(years > 1 ? "s" : "")}");
            if (months > 0) parts.Add($"{months} month{(months > 1 ? "s" : "")}");
            if (days > 0) parts.Add($"{days} day{(days > 1 ? "s" : "")}");

            return parts.Count > 0 ? string.Join(", ", parts) : "0 days";
        }
    }

    public bool IsCurrentEmployment => EndDate == null;

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
            StartDate = dto.StartDate;
            EndDate = dto.EndDate;
            Logo = dto.Logo;
            Url = dto.Url;
            UserId = dto.UserId;
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
        StartDate = startDate;
        EndDate = endDate;
        Logo = logo;
        Url = url;
    }

    public void EndEmployment(DateTime endDate)
    {
        if (endDate < StartDate)
            throw new ArgumentException("EndDate cannot be before StartDate.", nameof(endDate));

        if (EndDate.HasValue)
            throw new InvalidOperationException("Employment has already ended.");

        EndDate = endDate;
    }

    public void AddProject(string title, string description, IEnumerable<Skill>? skills = null)
    {
        ArgumentNullException.ThrowIfNull(title, nameof(title));
        ArgumentNullException.ThrowIfNull(description, nameof(description));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        var project = new Project
        {
            Title = title,
            Description = description,
            EmploymentId = this.Id,
            Employment = this
        };

        if (skills != null && skills.Any())
        {
            foreach (var skill in skills.Where(s => s != null))
            {
                project.AddSkill(skill);
            }
        }

        this.Projects.Add(project);
    }

    public void RemoveProject(int projectId)
    {
        var project = this.Projects.FirstOrDefault(p => p.Id == projectId);

        if (project == null)
            throw new InvalidOperationException($"Project with ID '{projectId}' not found in this employment.");

        this.Projects.Remove(project);
    }
}
