using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MainSceneUIManager : MonoBehaviour
{
    public static MainSceneUIManager instance;

    private Canvas canvas;

    [SerializeField]
    private RectTransform popupPanel;

    [SerializeField]
    private RectTransform planPanel;

    [SerializeField]
    private RectTransform contextMenu;
    private TimeSlot selectedTimeSlot;

    private void Awake()
    {
        instance = this;
        canvas = GetComponent<Canvas>();
        contextMenu.gameObject.SetActive(false);
    }

    public void OnCalendarButtonClicked()
    {
        popupPanel.gameObject.SetActive(true);
        planPanel.gameObject.SetActive(true);
    }

    public void OnTimeslotClicked(TimeSlot timeslot)
    {
        selectedTimeSlot = timeslot;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            null, // For a RectTransform in a Canvas set to Screen Space - Overlay mode, the cam parameter should be null.
            out Vector2 localPos
        );
        contextMenu.anchoredPosition = localPos + contextMenu.rect.size / 2;
        contextMenu.gameObject.SetActive(true);
    }

    public void OnContextMenuClicked(SlotType slotType)
    {
        selectedTimeSlot.SlotType = slotType;
        contextMenu.gameObject.SetActive(false);
    }

    public void OnCheckButtonClicked()
    {
        popupPanel.gameObject.SetActive(false);
        planPanel.gameObject.SetActive(false);
        contextMenu.gameObject.SetActive(false);
    }
}
