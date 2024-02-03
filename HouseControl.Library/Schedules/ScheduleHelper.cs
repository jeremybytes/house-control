using HouseControl.Sunset;

namespace HouseControl.Library;

public class ScheduleHelper
{
    private static ITimeProvider? timeProvider;
    public static ITimeProvider TimeProvider
    {
        get => timeProvider ??= new CurrentTimeProvider();
        set => timeProvider = value;
    }

    private readonly ISunsetProvider SunsetProvider;

    public ScheduleHelper(ISunsetProvider sunsetProvider)
    {
        this.SunsetProvider = sunsetProvider;
    }

    public static DateTimeOffset Now()
    {
        return TimeProvider.Now();
    }

    public static DateTime Today()
    {
        return TimeProvider.Now().Date;
    }

    public static DateTime Yesterday()
    {
        return TimeProvider.Now().Date.AddDays(-1);
    }

    public static TimeSpan DurationFromNow(DateTimeOffset checkTime)
    {
        return (checkTime - TimeProvider.Now()).Duration();
    }

    public static bool IsInPast(DateTimeOffset checkTime)
    {
        return checkTime < TimeProvider.Now();
    }

    public static bool IsInFuture(DateTimeOffset checkTime)
    {
        return checkTime > TimeProvider.Now();
    }

    public DateTimeOffset RollForwardToNextDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Today().AddDays(1);
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekdayDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Today() + TimeSpan.FromDays(1);
        while (nextDay.DayOfWeek == DayOfWeek.Saturday
            || nextDay.DayOfWeek == DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }

        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekendDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Today().AddDays(1);
        while (nextDay.DayOfWeek != DayOfWeek.Saturday
            && nextDay.DayOfWeek != DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay) + info.RelativeOffset,
            _ => info.EventTime
        };
    }
}
