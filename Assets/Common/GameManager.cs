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
        GameStateManager.SetNextDay();
        GameStateManager.SetCurrentTime(GameTime.Morning);
        GameStateManager.SetTimeslotForWeek(timeslots);

        ExecutePlan();
    }

    public void ExecutePlan()
    {
        //var plan = GameStateManager.GameState.CurrentPlan;
    }
}
