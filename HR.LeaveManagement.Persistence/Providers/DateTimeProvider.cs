namespace HR.LeaveManagement.Persistence.Providers;

public interface IDateTimeProvider
{
    public DateTime GetUtcNow();
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}