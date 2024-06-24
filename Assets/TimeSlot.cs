using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TimeSlot : MonoBehaviour
{
    private SlotType slotType;
    public SlotType SlotType
    {
        get { return slotType; }
        set
        {
            slotType = value;
            text.text = value.ToString();
        }
    }

    private TextMeshProUGUI text;

    public RectTransform RectTransform
    {
        get { return GetComponent<RectTransform>(); }
    }

    private void OnValidate()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Awake()
    {
        SlotType = SlotType.Empty;
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    public void OnClicked()
    {
        MainSceneUIManager.instance.OnTimeslotClicked(this);
    }
}
