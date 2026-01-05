using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Kr.Carevo.UMR.Persistence.Aggregate;

public sealed class EmploymentRepository(
    ILogger<EmploymentRepository> logger,
    CarevoDbContext dbContext,
    IMapper mapper)
    : BaseRepository<Employer>(logger, dbContext), IEmploymentRepository
{
    public async Task<IEnumerable<EmploymentResponseDto>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        // TO DO : Refactor to optimize query and reduce data load - using split queries and filtering but still needs to be optimised
        var employers = await DBset
            .AsNoTracking()
            .Include(e => e.UserEmployers.Where(ue => ue.UserId == userId))
                .ThenInclude(ue => ue.Projects)
                    .ThenInclude(p => p.Skills)
                    .AsSplitQuery()
            .Where(e => e.UserEmployers.Any(ue => ue.UserId == userId))
            .ToListAsync(cancellationToken);


        return mapper.Map<IEnumerable<EmploymentResponseDto>>(employers);
    }

    public async Task<ResponseDto> CreateUserEmploymentsAsync(
        int userId,
        IEnumerable<EmploymentDto> employments,
        CancellationToken cancellationToken = default)
    {
        var results = new ResponseDto();

        foreach (var dto in employments)
        {
            var employer = await DBset
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Company == dto.Company, cancellationToken);

            if (employer == null)
            {
                employer = await CreateNewEmployerAsync(dto, userId);
            }
            else
            {
                var employerUpdated = await HandleExistingEmployerAsync(employer, dto, userId, cancellationToken);
                if (employerUpdated == null)
                {                    
                    results.Responses.Add(ResponseItemDto.Create(employer!.Id, new Dictionary<string, object?>
                    {
                        { "Company", dto.Company }
                    }, ResponseStatus.Skipped));
                    continue;
                }
            }
           
            results.Responses.Add(ResponseItemDto.Create(employer!.Id, new Dictionary<string, object?>
                    {
                        { "Company", dto.Company }
                    }, ResponseStatus.Created));
        }

        await this.SaveAsync(cancellationToken);
        return results;
    }

    private async Task<Employer> CreateNewEmployerAsync(EmploymentDto dto, int userId)
    {
        var employer = new Employer();
        employer.CreateEmployer(dto);

        var userEmployer = new UserEmployer();
        userEmployer.CreateUserEmployment(dto, userId, employer.Id);

        employer.UserEmployers.Add(userEmployer);
        DBset.Add(employer);

        return employer;
    }

    private async Task<Employer?> HandleExistingEmployerAsync(
        Employer existingEmployer,
        EmploymentDto dto,
        int userId,
        CancellationToken cancellationToken)
    {
        var alreadyLinked = await DBset
            .Where(e => e.Id == existingEmployer.Id)
            .AnyAsync(e => e.UserEmployers.Any(ue => ue.UserId == userId), cancellationToken);

        if (alreadyLinked)
        {
            logger.LogWarning(
                "Employment for company {Company} already exists for user {UserId}. Skipping addition.",
                dto.Company, userId);

            return null; 
        }

        var employer = await DBset
            .Include(e => e.UserEmployers)
            .FirstAsync(e => e.Id == existingEmployer.Id, cancellationToken);

        var userEmployer = new UserEmployer();
        userEmployer.CreateUserEmployment(dto, userId, employer.Id);

        employer.UserEmployers.Add(userEmployer);
        DBset.Update(employer);

        return employer;
    }

 
}