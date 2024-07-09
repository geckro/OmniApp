namespace GameManager.Core.Data;

public static class Overloads
{
    public static Game Game(string title, Genre[]? genres = null, Platform[]? platforms = null, DateTime? date = null,
        Developer[]? developers = null, Publisher[]? publishers = null, Series[]? series = null)
        => new(title, genres, platforms, date, developers, publishers, series);

    public static Developer Dev(string name) => new(name);
    public static Genre G(string name) => new(name);
    public static Publisher P(string name) => new(name);
    public static Series S(string name) => new(name);
    public static Platform Pf(string name) => new(name);

    public static DateTime D(int year, int? month, int? day)
    {
        if (month.HasValue && day.HasValue)
            return new DateTime(year, month.Value, day.Value);
        return month.HasValue ? new DateTime(year, month.Value, 1).AddMonths(1).AddDays(-1) : new DateTime(year);
    }
}