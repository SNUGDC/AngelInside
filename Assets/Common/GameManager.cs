using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameStateManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameStateManager GameStateManager { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            GameStateManager = GetComponent<GameStateManager>();
            GameStateManager.ResetGameState();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void AddGameEssential()
    {
        SceneManager.LoadScene("GameEssential", LoadSceneMode.Additive);
    }

    public static void AddStatusBar()
    {
        SceneManager.LoadScene("StatusBar", LoadSceneMode.Additive);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="timeslots"></param>
    /// Initial date: Invoke PlanFinished on day 0. Game will start with day 1.
    public void MakePlan(Timeslot[,] timeslots)
    {
        GameStateManager.IncrementDay();
        GameStateManager.SetCurrentTime(GameTime.Morning);
        GameStateManager.SetTimeslotForWeek(timeslots);
    }

    public void ExecutePlan()
    {
        var plan = GameStateManager.GameState.CurrentPlan;
        switch (plan)
        {
            case Timeslot.Sprint:
                SceneManager.LoadSceneAsync("SprintMinigameScene", LoadSceneMode.Single);
                break;
            case Timeslot.Talk:
                GameStateManager.GameState.Energy -= 10;
                break;
            case Timeslot.Hangout:
                GameStateManager.GameState.Energy -= 10;
                GameStateManager.GameState.Stress -= 10;
                break;
            case Timeslot.Sleep:
                GameStateManager.GameState.Energy += 20;
                break;
        }
    }

    public void IncrementTime()
    {
        GameStateManager.IncrementTime();
    }

    public void MinigameFinish()
    {
        SceneManager.LoadSceneAsync("BedroomScene", LoadSceneMode.Single);
    }
}
