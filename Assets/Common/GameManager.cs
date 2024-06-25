using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void PlanFinished()
    {
        SceneManager.LoadScene("SprintGameScene");
    }
}
