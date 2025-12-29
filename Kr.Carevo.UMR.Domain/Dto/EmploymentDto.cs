namespace Kr.Carevo.UMR.Domain.Dto;

public class EmploymentDto
{
    public int? Id { get; init; }

    public required string Company { get; set; }

    public required DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Logo { get; set; }

    public string? Url { get; set; }

    public int UserId { get; init; }

    public IEnumerable<ProjectDto> Projects { get; set; } = [];
}
