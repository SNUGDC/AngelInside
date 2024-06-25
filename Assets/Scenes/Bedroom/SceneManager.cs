using UnityEngine;

namespace Bedroom
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GameManager.AddGameEssential();
            GameManager.AddStatusBar();
        }

        public void OnCheckButtonClicked()
        {
            GameManager.Instance.PlanFinished();
        }
    }
}
