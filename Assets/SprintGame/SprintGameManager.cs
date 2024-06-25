using UnityEngine;
using UnityEngine.Assertions;

public class SprintGameManager : MonoBehaviour
{
    public static SprintGameManager Instance { get; private set; }

    [SerializeField]
    private SprintGamePlayer player;

    [SerializeField]
    private SprintGameEnemy enemyPrefab;

    private void Awake()
    {
        Instance = this;
        Assert.IsNotNull(player, "Player is not set in SprintGameManager");
        Assert.IsNotNull(enemyPrefab, "Enemy prefab is not set in SprintGameManager");
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies), 0, 2);
    }

    void SpawnEnemies()
    {
        // Spawn enemies
        Instantiate(enemyPrefab, new Vector3(7f, -3f, 0), Quaternion.identity);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
