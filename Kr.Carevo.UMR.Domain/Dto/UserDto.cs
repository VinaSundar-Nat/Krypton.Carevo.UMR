using System;

namespace Kr.Carevo.UMR.Domain.Dto;

public class UserDto
{
    public int? Id { get; init; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required DateTime Dob { get; set; }

    public AddressDto? Address { get; set; }

    public IEnumerable<ContactDto> Contacts { get; set; } = [];

    public string Name { get; set; } = string.Empty;
}
