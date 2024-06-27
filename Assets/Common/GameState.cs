using System;
using UnityEngine.Assertions;

[System.Serializable]
public class GameState
{
    public int daysPlayed; // Start from 0, increment by 1 after GameTime.Evening
    public GameTime currentTime;
    public Timeslot[,] timeslots; // Maybe should contain (Plan, Result) pair

    // Player stats. Maybe should be in a separate class
    public int energy;
    public int stress;
    public XPLevel intelligence;

    public GameState()
    {
        daysPlayed = 0;
        currentTime = GameTime.Morning;
        timeslots = new Timeslot[GameConstants.TotalDays, GameConstants.TimeslotsPerDay];

        energy = 100;
        stress = 40;
        intelligence = new XPLevel(new uint[] { 0, 0, 100, 200, 300, 400, 500 });
    }

    public GameDate CurrentDate
    {
        get { return GameConstants.FirstDate + daysPlayed; }
    }
}

[System.Serializable]
public class GameDate
{
    public int month;
    public int day;

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

public enum GameTime
{
    Morning,
    Afternoon,
    Evening,
}

public enum Timeslot
{
    Empty,
    Sprint,
    Talk,
    Hangout,
    Sleep,
}

[System.Serializable]
public class XPLevel
{
    public uint xp = 0;

    // do not serialize xpNeeded
    readonly uint[] xpNeeded; // = new uint[] { 0, 0, 100, 200, 300, 400, 500 }

    public XPLevel(uint[] xpNeeded)
    {
        Assert.IsTrue(xpNeeded.Length >= 2);
        Assert.IsTrue(xpNeeded[0] == 0);
        Assert.IsTrue(xpNeeded[1] == 0);
        this.xpNeeded = xpNeeded;
    }

    public int Level
    {
        get
        {
            for (int i = xpNeeded.Length - 1; i >= 0; i--)
            {
                if (xp >= xpNeeded[i])
                    return i;
            }
            // It is asserted Level >= 1

            // Should never reach here
            return 0;
        }
    }

    public float Progress
    {
        get
        {
            if (Level == xpNeeded.Length - 1)
                return 1.0f;
            return (float)(xp - xpNeeded[Level]) / (xpNeeded[Level + 1] - xpNeeded[Level]);
        }
    }
}
