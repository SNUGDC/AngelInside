using System;

static class GameConstants
{
    public const int TotalWeeks = 42;
    public static readonly int DaysPerWeek = Enum.GetNames(typeof(GameWeekday)).Length;
    public static readonly int TimeslotsPerDay = Enum.GetNames(typeof(GameTime)).Length;

    public const int Year = 2024;
    public static readonly GameDate FirstDate = new() { month = 1, day = 1 };
    public const string SaveFilePath = "/save.json";

    // Derived constants
    public static readonly int TotalTimeslots = TotalWeeks * DaysPerWeek * TimeslotsPerDay;
    public static readonly int TotalDays = TotalWeeks * DaysPerWeek;
}
