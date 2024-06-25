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

        [SerializeField, Required]
        private RectTransform contextMenu;

        // User selected timeslot
        private TimeslotButton selectedTimeslot;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            contextMenu.gameObject.SetActive(false);
        }

        public void OnCalendarButtonClicked()
        {
            popupPanel.gameObject.SetActive(true);
            planPanel.gameObject.SetActive(true);
        }

        public void OnTimeslotClicked(TimeslotButton timeslot)
        {
            selectedTimeslot = timeslot;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                null, // For a RectTransform in a Canvas set to Screen Space - Overlay mode, the cam parameter should be null.
                out Vector2 localPos
            );
            contextMenu.anchoredPosition = localPos + contextMenu.rect.size / 2;
            contextMenu.gameObject.SetActive(true);
        }

        public void OnContextMenuClicked(Timeslot slotType)
        {
            selectedTimeslot.SlotType = slotType;
            contextMenu.gameObject.SetActive(false);
        }

        public void OnCheckButtonClicked()
        {
            popupPanel.gameObject.SetActive(false);
            planPanel.gameObject.SetActive(false);
            contextMenu.gameObject.SetActive(false);
            SceneManager.Instance.OnCheckButtonClicked();
        }
    }
}
