using UnityEngine;

namespace Bedroom
{
    [RequireComponent(typeof(UIManager))]
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GameManager.AddGameEssential();
            GameManager.AddStatusBar();
        }

        public void OnCheckButtonClicked(Timeslot[,] timeslots)
        {
            GameManager.Instance.MakePlan(timeslots);
        }
    }
}
