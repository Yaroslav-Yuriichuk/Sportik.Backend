namespace Sportik.Backend.Application.Helpers;

internal static class CalendarHelper
{
    public static DateTime GetFirstDayOfWeek(DateTime date)
    {
        int delta = (date.DayOfWeek - DayOfWeek.Monday + 7) % 7;
        return date.AddDays(-delta).Date;
    }

    public static DateTime GetLastDayOfWeek(DateTime date)
    {
        return GetFirstDayOfWeek(date).AddDays(6);
    }
}