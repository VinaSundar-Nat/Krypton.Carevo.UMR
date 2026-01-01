using System;
using MediatR;

namespace Kr.Carevo.UMR.Domain.Dto;

public sealed class UserDto
{
    public int? Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required DateTime Dob { get; set; }

    public AddressDto? Address { get; set; }

    public required ContactDto Contact { get; set; }

    public string Name { get; set; } = string.Empty;
}
