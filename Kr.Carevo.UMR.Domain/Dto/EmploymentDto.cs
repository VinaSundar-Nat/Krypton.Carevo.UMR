namespace Kr.Carevo.UMR.Domain.Dto;

public sealed class EmploymentDto
{
    public int? Id { get; set; }

    public required string Company { get; set; }

    public required DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Logo { get; set; }

    public string? Url { get; set; }

    public int UserId { get; set; }

    public required string Duration { get; set;}

    public IEnumerable<ProjectDto> Projects { get; set; } = [];
}
