using UnityEngine;

namespace Bedroom
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField, Required]
        private Canvas canvas;

        [SerializeField, Required]
        private RectTransform popupPanel;

        [SerializeField, Required]
        private RectTransform planPanel;

        private void OnValidate()
        {
            Required.Assert(this);
        }

        private void Awake() { }

        private void Start()
        {
            planPanel.GetComponent<PlanPanelUI>().OnPlanFinished = OnPlanFinished;
        }

        public void OnCalendarButtonClicked()
        {
            popupPanel.gameObject.SetActive(true);
            planPanel.gameObject.SetActive(true);
        }

        public void OnPlanFinished(Timeslot[,] timeslots)
        {
            popupPanel.gameObject.SetActive(false);
            planPanel.gameObject.SetActive(false);
            SceneManager.Instance.OnCheckButtonClicked(timeslots);
        }
    }
}
