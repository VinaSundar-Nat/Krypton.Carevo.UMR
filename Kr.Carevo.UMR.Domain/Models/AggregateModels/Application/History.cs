using Kr.Common.Infrastructure.Datastore;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public class ApplicationStatusHistory : BaseEntity<ApplicationStatusHistory>
{
    public ApplicationStatus Status { get; set; }
    public ApplicationStatus? PreviousStatus { get; set; }
    public DateTime StatusChangedDate { get; set; }
    public string? Notes { get; set; }
    public string? ChangedBy { get; set; }
    public string? Reason { get; set; }
    public int ApplicationId { get; set; }
    public Application? Application { get; set; }

    public void CreateHistory(ApplicationStatus newStatus, ApplicationStatus? previousStatus = null, string? notes = null, string? reason = null, string? changedBy = null)
    {
        if (newStatus == previousStatus)
            throw new ArgumentException("New status cannot be the same as previous status.", nameof(newStatus));

        Status = newStatus;
        PreviousStatus = previousStatus;
        StatusChangedDate = DateTime.UtcNow;
        Notes = notes;
        Reason = reason;
        ChangedBy = changedBy;
    }
}
