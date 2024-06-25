using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bedroom
{
    [RequireComponent(typeof(Button))]
    public class TimeslotButton : MonoBehaviour
    {
        private Timeslot slotType;
        public Timeslot SlotType
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
            SlotType = Timeslot.Empty;
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(OnClicked);
        }

        public void OnClicked()
        {
            UIManager.Instance.OnTimeslotClicked(this);
        }
    }
}
