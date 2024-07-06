using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

public class GameStateManager : MonoBehaviour
{
    public GameState CurrentGameState
    {
        get { return GameState; }
    }

    public GameState GameState { get; private set; }

    public void SaveGameState()
    {
        string json = JsonUtility.ToJson(GameState);
        File.WriteAllText(Application.persistentDataPath + GameConstants.SaveFilePath, json);
    }

    public void LoadGameState()
    {
        string json = File.ReadAllText(Application.persistentDataPath + GameConstants.SaveFilePath);
        GameState = JsonUtility.FromJson<GameState>(json);
    }

    // Actions for changing game state
    public void ResetGameState()
    {
        GameState = GameState.InitialGameState();
    }

    public void IncrementDay()
    {
        GameState.DaysPlayed++;
    }

    public void SetCurrentTime(GameTime time)
    {
        GameState.CurrentTime = time;
    }

    public void IncrementTime()
    {
        if (GameState.CurrentTime == GameTime.Evening)
        {
            IncrementDay();
            GameState.CurrentTime = GameTime.Morning;
        }
        else
        {
            GameState.CurrentTime++;
        }
    }

    /// <summary>
    /// Set timeslots for the week [DaysPlayed, DaysPlayed + 6]
    /// </summary>
    /// <param name="timeslots"></param>
    public void SetTimeslotForWeek(Timeslot[,] timeslots)
    {
        Assert.AreEqual(GameConstants.DaysPerWeek, timeslots.ColLength());
        int day = GameState.DaysPlayed;

        for (int i = 0; i < timeslots.ColLength(); i++)
        {
            SetTimeslotDay(day + i, timeslots.Col(i));
        }
    }

    private void SetTimeslotDay(int day, Timeslot[] timeslots)
    {
        Assert.AreEqual(GameConstants.TimeslotsPerDay, timeslots.Length);

        for (int i = 0; i < timeslots.Length; i++)
        {
            GameState.Timeslots[i, day] = timeslots[i];
        }
    }
}
