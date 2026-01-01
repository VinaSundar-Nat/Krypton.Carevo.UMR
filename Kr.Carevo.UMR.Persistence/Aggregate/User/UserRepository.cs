using System;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Infrastructure.Datastore;
using Kr.Carevo.UMR.Domain.Models.AggregateModels;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Kr.Carevo.UMR.Persistence.Aggregate;

public class UserRepository(ILogger<UserRepository> logger, CarevoDbContext dbContext) : 
        BaseRepository<User>(logger, dbContext), IUserRepository
{
    public async Task<UserDto> Create(UserDto user, CancellationToken cancellationToken = default)
    {
        var entity = new User();
        entity.CreateUser(user);
        this.DBset.Add(entity);
        await this.SaveAsync(cancellationToken);
        user.Id = entity.Id;
        return user;
    }

    public async Task<bool> ExistsByContactAsync(string contactValue, CancellationToken cancellationToken = default)
    {
        return await DBset
            .AsNoTracking()
            .SelectMany(u => u.Contacts)  
            .AnyAsync(c => c.Value == contactValue, cancellationToken);    
    }

  
}