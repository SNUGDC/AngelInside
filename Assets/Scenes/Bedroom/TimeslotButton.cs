using UnityEngine;

namespace Bedroom
{
    [RequireComponent(typeof(TextButtonUI))]
    public class TimeslotButton : MonoBehaviour
    {
        private TextButtonUI textButton;

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
            textButton = GetComponent<TextButtonUI>();
            //textButton.AddListener(OnClicked); // TOOD: Timing of TextButton Awake() and this Awake() ?
            SlotType = Timeslot.Empty; // Setter should appear after textButton is initialized
        }

        public void OnClicked()
        {
            UIManager.Instance.OnTimeslotClicked(this);
        }
    }
}
