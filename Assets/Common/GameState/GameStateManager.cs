using System.IO;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public static GameState CurrentGameState
    {
        get { return Instance.GameState; }
    }

    public GameState GameState { get; private set; }

    private void Awake()
    {
        Instance = this;
        ResetGameState();
    }

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
}
