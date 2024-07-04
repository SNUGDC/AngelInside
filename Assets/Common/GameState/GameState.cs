[System.Serializable]
public class GameState
{
    public int DaysPlayed { get; private set; } // Start from 0, increment by 1 after GameTime.Evening
    public GameTime CurrentTime { get; private set; }
    public Timeslot[,] Timeslots { get; private set; } // Maybe should contain (Plan, Result) pair

    // Player stats. Maybe should be packed in a separate class
    public int Energy { get; private set; }
    public int Stress { get; private set; }
    public XPLevel Intelligence { get; private set; }

    public static GameState InitialGameState()
    {
        return new GameState
        {
            DaysPlayed = 0,
            CurrentTime = GameTime.Morning,
            Timeslots = new Timeslot[GameConstants.TotalDays, GameConstants.TimeslotsPerDay],

            Energy = 100,
            Stress = 40,
            Intelligence = new XPLevel(new uint[] { 0, 0, 100, 200, 300, 400, 500 })
        };
    }

    public GameDate CurrentDate
    {
        get { return GameConstants.FirstDate + DaysPlayed; }
    }
}
