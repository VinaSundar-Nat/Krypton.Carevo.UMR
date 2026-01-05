namespace Kr.Carevo.UMR.Domain.Dto;


public abstract class EmploymentBaseDto
{
    public required string Company { get; set; }

    public required DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Duration { get; set; } = string.Empty;

    public string? Logo { get; set; }

    public string? Url { get; set; }

    public IEnumerable<ProjectDto> Projects { get; set; } = [];
}


public sealed class EmploymentDto : EmploymentBaseDto
{
    public int? Id { get; set; }

    public int UserId { get; set; }
}

public sealed class EmploymentResponseDto : EmploymentBaseDto
{
    public required int Id { get; set; }
}

