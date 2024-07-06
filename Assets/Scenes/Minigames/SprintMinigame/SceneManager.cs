using UnityEngine;
using UnityEngine.Assertions;

namespace SprintGame
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [SerializeField, Required]
        private Player player;

        [SerializeField, Required]
        private Enemy enemyPrefab;

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
            GameManager.Instance.MinigameFinish();
        }
    }
}
