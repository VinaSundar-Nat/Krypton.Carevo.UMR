namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public sealed class UserEmployer
{
    public int Id { get; private set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public int EmployerId { get; set; }
    public Employer? Employer { get; set; }

    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public ICollection<Project> Projects { get; private set; } = [];

     public string Duration
    {
        get
        {
            var end = EndDate ?? DateTime.UtcNow;
            var duration = end - StartDate;
            var years = duration.Days / 365;
            var months = (duration.Days % 365) / 30;
            var days = (duration.Days % 365) % 30;

            var parts = new List<string>();
            if (years > 0) parts.Add($"{years} year{(years > 1 ? "s" : "")}");
            if (months > 0) parts.Add($"{months} month{(months > 1 ? "s" : "")}");
            if (days > 0) parts.Add($"{days} day{(days > 1 ? "s" : "")}");

            return parts.Count > 0 ? string.Join(", ", parts) : "0 days";
        }
    }

     public bool IsCurrentEmployment => EndDate == null;

    public void EndEmployment(DateTime endDate)
    {
        if (endDate < StartDate)
            throw new ArgumentException("EndDate cannot be before StartDate.", nameof(endDate));

        if (EndDate.HasValue)
            throw new InvalidOperationException("Employment has already ended.");

        EndDate = endDate;
    }

     public void AddProject(string title, string description, IEnumerable<Skill>? skills = null)
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
            UserEmployerId = this.Id,
        };

        if (skills != null && skills.Any())
        {
            foreach (var skill in skills.Where(s => s != null))
            {
                project.AddSkill(skill);
            }
        }

        this.Projects.Add(project);
    }

       public void RemoveProject(int projectId)
    {
        var project = this.Projects.FirstOrDefault(p => p.Id == projectId);

        if (project == null)
            throw new InvalidOperationException($"Project with ID '{projectId}' not found in this employment.");

        this.Projects.Remove(project);
    }

  
}