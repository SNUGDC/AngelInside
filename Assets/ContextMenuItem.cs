using TMPro;
using UnityEngine;

public class ContextMenuItem : MonoBehaviour
{
    public SlotType slotType;

    private void Awake()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClicked);
        GetComponentInChildren<TextMeshProUGUI>().text = slotType.ToString();
    }

    private void OnClicked()
    {
        MainSceneUIManager.instance.OnContextMenuClicked(slotType);
    }
}
