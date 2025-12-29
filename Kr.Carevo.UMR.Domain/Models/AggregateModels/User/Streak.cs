using Kr.Common.Infrastructure.Datastore;

namespace Kr.Carevo.UMR.Domain.Models.AggregateModels;

public class Streak : BaseEntity<Streak>
{
    public required DateTime ActivityDate { get; set; }
    public int ApplicationCount { get; private set; } = 0;
    public int? ConsecutiveDayCount { get; private set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public bool IsConsecutiveDay => ConsecutiveDayCount.HasValue && ConsecutiveDayCount > 0;
    public bool IsToday => ActivityDate.Date == DateTime.UtcNow.Date;
    public int DaysSinceActivity => (int)(DateTime.UtcNow.Date - ActivityDate.Date).TotalDays;

    public void RecordActivity(DateTime activityDate, int userId, int applicationCount = 0)
    {
        if (activityDate == default)
            throw new ArgumentException("ActivityDate must be a valid date.", nameof(activityDate));

        if (userId <= 0)
            throw new ArgumentException("UserId must be valid.", nameof(userId));

        ActivityDate = activityDate.Date;
        UserId = userId;
        ApplicationCount = applicationCount;
    }

    public void IncrementApplicationCount(int count = 1)
    {
        if (count < 1)
            throw new ArgumentException("Count must be at least 1.", nameof(count));

        ApplicationCount += count;
    }

    public void SetConsecutiveDay(int consecutiveCount)
    {
        if (consecutiveCount < 1)
            throw new ArgumentException("ConsecutiveDayCount must be at least 1.", nameof(consecutiveCount));

        ConsecutiveDayCount = consecutiveCount;
    }

    public void ResetConsecutiveDay()
    {
        ConsecutiveDayCount = null;
    }
}

