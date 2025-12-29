using System;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;

namespace Kr.Carevo.UMR.Persistence.Aggregate.User;

public class UserRepository:IUserRepository
{
    public async Task<UserDto> Create(UserDto user)
    {
        // Implementation for creating a user in the database
        throw new NotImplementedException();
    }
}