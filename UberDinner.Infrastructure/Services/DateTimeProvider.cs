
using UberDinner.Application.Common.Interfaces.Services;

namespace UberDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
