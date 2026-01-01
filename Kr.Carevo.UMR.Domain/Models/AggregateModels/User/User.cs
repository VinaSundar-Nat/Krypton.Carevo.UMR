using Kr.Carevo.UMR.Domain.Dto;
using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Interface;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public enum UserStatus
{
    Active,
    Inactive,
    Suspended,
    Pending
}

public sealed class User : BaseEntity<User>, IAggregateRoot
{
    public  string FirstName { get; private set; } = string.Empty!;
    public  string LastName { get; private set; } = string.Empty!;
    public DateTime Dob { get; private set; }
    public UserStatus Status { get; private set; } 
    public Address? ResidentialAddress { get; private set; }
    public ICollection<Contact> Contacts { get; private set; } = [];
    public ICollection<Skill> Skills { get; set; } =[];
    // Many-to-many relationships
    public ICollection<UserSkill> UserSkills { get; private set; } = [];

    public ICollection<Employment> Employments { get; private set; } = [];
    public ICollection<Project> IndividualProjects { get; private set; } = [];
    public ICollection<Application> Applications { get; private set; } = [];
    public ICollection<Streak> ActivityStreaks { get; private set; } = [];

    public void CreateUser( UserDto userDto)
    {
        FirstName = userDto.FirstName;
        LastName = userDto.LastName;
        Dob = userDto.Dob;
        Status = UserStatus.Pending;

        if (userDto.Address == null)
            return;

        ResidentialAddress = new Address
        {
            Line1 = userDto.Address!.Line1!,
            Line2 = userDto.Address!.Line2,
            Suburb = userDto.Address!.Suburb,
            City = userDto.Address!.City!,
            State = userDto.Address!.State!,
            PostCode = userDto.Address!.PostCode!,
            Country = userDto.Address!.Country!,
            Coordinates = userDto.Address!.Latitude.HasValue && userDto.Address!.Longitude.HasValue
                ? new Coordinates(userDto.Address!.Latitude.Value, userDto.Address!.Longitude.Value)
                : null
        };

        if (userDto.Contact.IsValid)
            AddContact(userDto.Contact);
    }

    public void UpdateUserStatus( int UserId, UserStatus status)
    {
        if (this.Id != UserId)
        {
            throw new ArgumentException("User ID does not match.");
        }

        Status = status;
    }

    private static IEnumerable<Contact> CreateContactsFromDto(ContactDto contactDto)
    {
        if (!string.IsNullOrWhiteSpace(contactDto.Email))
            yield return new Contact { Type = ContactType.Email, Value = contactDto.Email };

        if (!string.IsNullOrWhiteSpace(contactDto.PhoneNumber))
            yield return new Contact { Type = ContactType.Phone, Value = contactDto.PhoneNumber };

        if (!string.IsNullOrWhiteSpace(contactDto.MobileNumber))
            yield return new Contact { Type = ContactType.Mobile, Value = contactDto.MobileNumber };
    }

    public void AddContact(ContactDto contactDto)
    {
        var potentialContacts = CreateContactsFromDto(contactDto);

        foreach (var contact in potentialContacts)
        {
            this.Contacts.Add(contact);
        }
    }

    public void AddSkill(string code, string description, DateTime effectiveDate)
    {
        ArgumentNullException.ThrowIfNull(code, nameof(code));
        ArgumentNullException.ThrowIfNull(description, nameof(description));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Code cannot be empty.", nameof(code));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        if (effectiveDate == default)
            throw new ArgumentException("EffectiveDate must be a valid date.", nameof(effectiveDate));

        if (this.Skills.Any(s => s.Code.Equals(code, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"A skill with code '{code}' already exists for this user.");

        var skill = new Skill
        {
            Code = code,
            Description = description,
            EffectiveDate = effectiveDate
        };

        this.Skills.Add(skill);
    }

    public void RemoveSkill(int skillId)
    {
        var skill = this.Skills.FirstOrDefault(s => s.Id == skillId);

        if (skill == null)
            throw new InvalidOperationException($"Skill with ID '{skillId}' not found for this user.");

        this.Skills.Remove(skill);
    }
   
    public void RemoveEmployment(int employmentId)
    {
        var employment = this.Employments.FirstOrDefault(e => e.Id == employmentId);

        if (employment == null)
            throw new InvalidOperationException($"Employment with ID '{employmentId}' not found for this user.");

        this.Employments.Remove(employment);
    }

    public void AddIndividualProject(string title, string description, IEnumerable<Skill>? skills = null)
    {
        ArgumentNullException.ThrowIfNull(title, nameof(title));
        ArgumentNullException.ThrowIfNull(description, nameof(description));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        var project = new Project
        {
            Title = title,
            Description = description,
            UserId = this.Id,
            User = this
        };

        if (skills != null && skills.Any())
        {
            foreach (var skill in skills.Where(s => s != null))
            {
                project.AddSkill(skill);
            }
        }

        this.IndividualProjects.Add(project);
    }

    public void RemoveIndividualProject(int projectId)
    {
        var project = this.IndividualProjects.FirstOrDefault(p => p.Id == projectId);

        if (project == null)
            throw new InvalidOperationException($"Individual project with ID '{projectId}' not found for this user.");

        this.IndividualProjects.Remove(project);
    }


    public Streak? GetTodayActivityStreak()
        => this.ActivityStreaks
            .FirstOrDefault(s => s.ActivityDate == DateTime.UtcNow.Date);

    public int GetCurrentConsecutiveStreak()
    {
        var today = DateTime.UtcNow.Date;
        var streak = this.ActivityStreaks.FirstOrDefault(s => s.ActivityDate == today);
        return streak?.ConsecutiveDayCount ?? 0;
    }

    public int GetLongestConsecutiveStreak()
    {
        return this.ActivityStreaks
            .Where(s => s.IsConsecutiveDay)
            .Max(s => s.ConsecutiveDayCount ?? 1);
    }

    
}