
using Kr.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kr.Carevo.UMR.Persistence.Aggregate;
public sealed class ApplicationRepository(
    ILogger<ApplicationRepository> logger,
    CarevoDbContext dbContext,
    IMapper mapper)
    : BaseRepository<Application>(logger, dbContext), IApplicationRepository
{
    public async Task<ApplicationResponseDto> CreateAsync(int userId, ApplicationDto applicationDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var application = new Application();
            application.CreateApplication(userId, applicationDto.JobId, applicationDto.Notes ?? string.Empty);

            DBset.Add(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<ApplicationResponseDto>(application);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating application for user {UserId} and job {JobId}", userId, applicationDto.JobId);
            throw;
        }
    }

    public async Task<IEnumerable<ApplicationResponseDto>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await DBset.AsNoTracking()
            .Where(u => u.UserId == userId)
            .ProjectTo<ApplicationResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<ApplicationResponseDto?> GetByIdAsync(int applicationId, CancellationToken cancellationToken = default)
    {
        return await DBset.AsNoTracking()
            .Where(a => a.Id == applicationId)
            .ProjectTo<ApplicationResponseDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsByUserAndJobIdAsync(int userId, string jobId, CancellationToken cancellationToken = default)
    {
        return await DBset.AsNoTracking()
            .AnyAsync(a => a.UserId == userId && a.JobId == jobId, cancellationToken);
    }

    public async Task<bool> IsApplicationOwnedByUserAsync(int applicationId, int userId, CancellationToken cancellationToken = default)
    {
        return await DBset.AsNoTracking()
            .AnyAsync(a => a.Id == applicationId && a.UserId == userId, cancellationToken);
    }

    public async Task<bool> UpdateApplicationStatusAsync(int applicationId, int userId, ApplicationStatusDto statusChange, CancellationToken cancellationToken = default)
    {
        try
        {
            var application = await DBset
                .FirstOrDefaultAsync(a => a.Id == applicationId && a.UserId == userId, cancellationToken) ?? throw new InvalidOperationException($"Application {applicationId} not found for user {userId}");
            
            application.UpdateApplicationStatus(MapStatus(statusChange.Status), statusChange.Notes, statusChange.StatusChangedDate);

            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating application {ApplicationId} status for user {UserId}", applicationId, userId);
            throw;
        }
    }

    private ApplicationStatus MapStatus(string status)
        =>  status.ToLower() switch
        {
            "applied" => ApplicationStatus.Applied,
            "underreview" => ApplicationStatus.UnderReview,
            "shortlisted" => ApplicationStatus.Shortlisted,
            "interviewed" => ApplicationStatus.Interviewed,
            "offered" => ApplicationStatus.Accepted,
            "rejected" => ApplicationStatus.Rejected,
            "accepted" => ApplicationStatus.Accepted,
            "withdrawn" => ApplicationStatus.Withdrawn,
            "saved" => ApplicationStatus.Saved,
            "archived" => ApplicationStatus.Archived,
            _ => throw new DomainValidationException($"Invalid application status", failures:
            [
                new("ApplicationStatus", $"Invalid application status: {status}")
            ])           
        };
}

