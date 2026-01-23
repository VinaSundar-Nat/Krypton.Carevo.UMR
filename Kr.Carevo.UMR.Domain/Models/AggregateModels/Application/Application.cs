
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

public sealed class Application : BaseEntity<Application>, IAggregateRoot
{
    public string JobId { get; private set; } = string.Empty!;
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

    public void CreateApplication(int userId, string jobId, string notes)
    {
        JobId = jobId;
        UserId = userId;
        Status = ApplicationStatus.Applied;
        AppliedDate = DateTime.UtcNow;
        Notes = notes;

        // Record initial status
        RecordStatusChange(ApplicationStatus.Applied, null, notes, string.Empty, null, AppliedDate);
    }

    public void UpdateApplicationStatus(ApplicationStatus newStatus, string? notes = null, DateTime? statusChangeDate = null)
    {
        if (Status == ApplicationStatus.Rejected || Status == ApplicationStatus.Withdrawn)
            DomainExceptions.ThrowDomainException("Invalid Status Update", ("ApplicationStatus", $"Cannot update status for a {Status} application."));
           
        var previousStatus = Status;
        Status = newStatus;

         if (newStatus == previousStatus && notes?.ToLower() == Notes?.ToLower())
            DomainExceptions.ThrowDomainException("Invalid Status Update", ("ApplicationStatus", "No changes detected in status or notes."));

        if (!string.IsNullOrWhiteSpace(notes))
            Notes = notes;

        // Record status change in history
        RecordStatusChange(newStatus, previousStatus, notes, null, null, statusChangeDate);
    }

    public void WithdrawApplication(string? reason = null)
    {
        if (Status == ApplicationStatus.Withdrawn)
            DomainExceptions.ThrowDomainException("Invalid Operation", ("ApplicationStatus", "Application has already been withdrawn."));

        if (Status == ApplicationStatus.Rejected)
            DomainExceptions.ThrowDomainException("Invalid Operation", ("ApplicationStatus", "Cannot withdraw a rejected application."));
        var previousStatus = Status;
        Status = ApplicationStatus.Withdrawn;
        Notes = reason;

        // Record withdrawal in history
        RecordStatusChange(ApplicationStatus.Withdrawn, previousStatus, reason, "Application withdrawn");
    }

    public void AddNotes(string notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
            DomainExceptions.ThrowDomainException("Invalid Operation", ("Notes", "Notes cannot be empty."));

        Notes = notes;
    }

    private void RecordStatusChange(ApplicationStatus newStatus, ApplicationStatus? previousStatus = null, string? notes = null, string? reason = null, string? changedBy = null, DateTime? statusChangeDate = null)
    {
        var history = new ApplicationStatusHistory();
        history.CreateHistory(newStatus, previousStatus, notes, reason, changedBy, statusChangeDate);
        history.ApplicationId = this.Id;
        
        this.StatusHistory.Add(history);
    }

}