namespace Kr.Carevo.UMR.Domain.Dto;

public sealed class ContactDto
{
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }

    public bool IsValid => !(string.IsNullOrWhiteSpace(Email) &&
                          string.IsNullOrWhiteSpace(PhoneNumber) &&
                          string.IsNullOrWhiteSpace(MobileNumber));
}