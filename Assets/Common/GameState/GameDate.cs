using System;

[System.Serializable]
public class GameDate
{
    public int month;
    public int day;

    public GameWeekday Weekday
    {
        get
        {
            DateTime date = new(GameConstants.Year, month, day);
            return (GameWeekday)((int)date.DayOfWeek);
        }
    }

    public static GameDate operator +(GameDate date, int days)
    {
        int totalDays = date.ToDays() + days;
        return FromDays(totalDays);
    }

    public static int DaysInMonth(int month)
    {
        return DateTime.DaysInMonth(GameConstants.Year, month);
    }

    public static GameDate FromDays(int days)
    {
        int month = 1;
        while (days > DaysInMonth(month))
        {
            days -= DaysInMonth(month);
            month++;
        }
        return new GameDate { month = month, day = days };
    }

    public int ToDays()
    {
        int days = 0;
        for (int i = 1; i < month; i++)
        {
            days += DaysInMonth(i);
        }
        return days + day;
    }
}
