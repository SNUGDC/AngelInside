using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bedroom
{
    public class ContextMenuItem : MonoBehaviour
    {
        public Timeslot slotType;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClicked);
            GetComponentInChildren<TextMeshProUGUI>().text = slotType.ToString();
        }

        private void OnClicked()
        {
            UIManager.Instance.OnContextMenuClicked(slotType);
        }
    }
}
