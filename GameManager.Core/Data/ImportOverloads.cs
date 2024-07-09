namespace GameManager.Core.Data;

public static class Overloads
{
    public static Game Game(string title, Genre[]? genres = null, Platform[]? platforms = null, DateTime? dateWw = null,
        Developer[]? developers = null, Publisher[]? publishers = null, Series[]? series = null)
    {
        return new Game(title, genres, platforms, developers, publishers, series, dateWw);
    }

    public static Developer Dev(string name)
    {
        return new Developer(name);
    }

    public static Genre G(string name)
    {
        return new Genre(name);
    }

    public static Publisher P(string name)
    {
        return new Publisher(name);
    }

    public static Series S(string name)
    {
        return new Series(name);
    }

    public static Platform Pf(string name)
    {
        return new Platform(name);
    }

    public static DateTime D(int year, int? month, int? day)
    {
        if (month.HasValue && day.HasValue)
        {
            return new DateTime(year, month.Value, day.Value);
        }

        return month.HasValue ? new DateTime(year, month.Value, 1).AddMonths(1).AddDays(-1) : new DateTime(year);
    }
}
