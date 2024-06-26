using UnityEngine;

namespace Bedroom
{
    [RequireComponent(typeof(TextButton))]
    public class TimeslotButton : MonoBehaviour
    {
        private TextButton textButton;

        private Timeslot slotType;
        public Timeslot SlotType
        {
            get { return slotType; }
            set
            {
                slotType = value;
                textButton.Text = value.ToString();
            }
        }

        private void Awake()
        {
            textButton = GetComponent<TextButton>();
            textButton.AddListener(OnClicked);
            SlotType = Timeslot.Empty; // Setter should appear after textButton is initialized
        }

        public void OnClicked()
        {
            UIManager.Instance.OnTimeslotClicked(this);
        }
    }
}
