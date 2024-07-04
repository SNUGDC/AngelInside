using UnityEngine;

namespace Bedroom
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField, Required]
        private Canvas canvas;

        [SerializeField, Required]
        private RectTransform popupPanel;

        [SerializeField, Required]
        private RectTransform planPanel;

        // User selected timeslot
        private TimeslotButton selectedTimeslot;

        private void OnValidate()
        {
            Required.Assert(this);
        }

        private void Awake()
        {
            Instance = this;
        }

        public void OnCalendarButtonClicked()
        {
            popupPanel.gameObject.SetActive(true);
            planPanel.gameObject.SetActive(true);
        }

        public void OnTimeslotClicked(TimeslotButton timeslot)
        {
            selectedTimeslot = timeslot;
        }

        public void OnContextMenuClicked(Timeslot slotType)
        {
            selectedTimeslot.SlotType = slotType;
        }

        public void OnCheckButtonClicked()
        {
            popupPanel.gameObject.SetActive(false);
            planPanel.gameObject.SetActive(false);
            SceneManager.Instance.OnCheckButtonClicked();
        }
    }
}
