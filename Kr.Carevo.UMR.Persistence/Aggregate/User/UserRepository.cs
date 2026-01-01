using System;
using Kr.Carevo.UMR.Domain.Dto;
using Kr.Carevo.UMR.Domain.Ports;
using Kr.Common.Infrastructure.Datastore;
using Kr.Carevo.UMR.Domain.Models.AggregateModels;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Kr.Common.Exceptions;
using FluentValidation.Results;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Kr.Carevo.UMR.Persistence.Aggregate;

public class UserRepository(ILogger<UserRepository> logger, CarevoDbContext dbContext, IMapper mapper) :
        BaseRepository<User>(logger, dbContext), IUserRepository
{
    public async Task<UserDto> CreateAsync(UserDto user, CancellationToken cancellationToken = default)
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

    public async Task<IEnumerable<UserResponseDto>> GetDetailsByIdAsync(int value, CancellationToken cancellationToken = default)
    {
        return await DBset
            .Where(u => u.Id == value)
            .ProjectTo<UserResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

      public async Task<IEnumerable<UserResponseDto>> GetDetailsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DBset
            .Where(u => u.Contacts.Any(c => c.Type == ContactType.Email && c.Value == email))
            .ProjectTo<UserResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SkillDto>> UpdateSkillsAsync(int userId, IEnumerable<SkillDto> skills, CancellationToken cancellationToken = default)
    {
        var user = await DBset
            .Include(u => u.Skills)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                ?? throw new DomainValidationException($"User not found.", failures:
            [
                new("UserId", $"User with ID {userId} does not exist.")
            ]);

        foreach (var skill in skills)
        {
            if (user.Skills.Any(s => s.Code.Equals(skill.Code, StringComparison.OrdinalIgnoreCase)))
            {
                logger.LogWarning("Skill with code {SkillCode} already exists for user {UserId}. Skipping addition.", skill.Code, userId);
                continue;
            }

            user.AddSkill(skill);
        }


        await this.SaveAsync(cancellationToken);

        return skills.Select(s => new SkillDto
        {
            Id = user.Skills.First(us => us.Code == s.Code).Id,
            Code = s.Code,
            Description = s.Description,
            EffectiveDate = s.EffectiveDate
        });
    }
  
}