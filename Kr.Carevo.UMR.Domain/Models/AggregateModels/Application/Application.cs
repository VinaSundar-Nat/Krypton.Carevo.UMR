using Kr.Common.Infrastructure.Datastore;
using Kr.Common.Infrastructure.Datastore.Interface;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public enum ApplicationStatus
{
    Applied,
    UnderReview,
    Shortlisted,
    Interviewed,
    Accepted,
    Rejected,
    Withdrawn,
    Saved,
    Archived
}

public class Application : BaseEntity<Application>, IAggregateRoot
{
    public int JobId { get; private set; }
    public bool IsActive => Status != ApplicationStatus.Rejected && Status != ApplicationStatus.Withdrawn;
    public ApplicationStatus Status { get; private set; }
    public DateTime AppliedDate { get; private set; }
    public string? Notes { get; private set; }
    // Agent Personalized self Employment Data for Job if 
    // Application is created from Job
    public string? PersonalizedEmploymentData { get; private set; }
    public int UserId { get; private set; }
    public User? User { get; set; }
    public IList<ApplicationStatusHistory> StatusHistory { get; private set; } = [];

    public void CreateApplication(int userId, int jobId, string notes, ApplicationStatus status)
    {
        JobId = jobId;
        UserId = userId;
        Status = status;
        AppliedDate = DateTime.UtcNow;
        Notes = notes;

        // Record initial status
        RecordStatusChange(status, null, notes, string.Empty);
    }

    public void UpdateApplicationStatus(ApplicationStatus newStatus, string? notes = null)
    {
        if (Status == ApplicationStatus.Rejected || Status == ApplicationStatus.Withdrawn)
            throw new InvalidOperationException($"Cannot update status for a {Status} application.");

        var previousStatus = Status;
        Status = newStatus;

        if (!string.IsNullOrWhiteSpace(notes))
            Notes = notes;

        // Record status change in history
        RecordStatusChange(newStatus, previousStatus, notes);
    }

    public void WithdrawApplication(string? reason = null)
    {
        if (Status == ApplicationStatus.Withdrawn)
            throw new InvalidOperationException("Application has already been withdrawn.");

        if (Status == ApplicationStatus.Rejected)
            throw new InvalidOperationException("Cannot withdraw a rejected application.");

        var previousStatus = Status;
        Status = ApplicationStatus.Withdrawn;
        Notes = reason;

        // Record withdrawal in history
        RecordStatusChange(ApplicationStatus.Withdrawn, previousStatus, reason, "Application withdrawn");
    }

    public void AddNotes(string notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
            throw new ArgumentException("Notes cannot be empty.", nameof(notes));

        Notes = notes;
    }

    private void RecordStatusChange(ApplicationStatus newStatus, ApplicationStatus? previousStatus = null, string? notes = null, string? reason = null, string? changedBy = null)
    {
        var history = new ApplicationStatusHistory();
        history.CreateHistory(newStatus, previousStatus, notes, reason, changedBy);
        history.ApplicationId = this.Id;
        history.Application = this;
        
        this.StatusHistory.Add(history);
    }

}