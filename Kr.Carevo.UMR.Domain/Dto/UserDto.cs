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
}

public sealed class UserResponseDto
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime Dob { get; set; }
    public string Name => $"{FirstName} {LastName}";
    public required ContactDto Contact { get; set; } 
    public required string Status { get; set; }
    public IEnumerable<String> Skills { get; set; } = [];
}



