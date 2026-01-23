using System.ComponentModel.DataAnnotations;

namespace Kr.Carevo.UMR.Domain.Dto;

public class ApplicationDto
{
    public required int UserId { get; set; }

    public required string JobId { get; set; }

    public required string Status { get; set; }
    
    public required DateTime AppliedDate { get; set; }

     public string? Notes { get; set; }
}


public sealed class ApplicationResponseDto : ApplicationDto
{
    public required int Id { get; set; }
    public string? PersonalizedEmploymentData { get; set; }
    public IEnumerable<ApplicationStatusDto> StatusHistory { get; set; } = [];
}

public sealed record ApplicationStatusDto(
    [Required] string Status,
    [Required] DateTime StatusChangedDate,
    string? Notes
);