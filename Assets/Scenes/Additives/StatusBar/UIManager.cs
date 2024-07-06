using TMPro;
using UnityEngine;

public class StatusBarUIManager : MonoBehaviour
{
    [SerializeField, Required]
    TextMeshProUGUI gamedateText;

    [SerializeField, Required]
    TextMeshProUGUI[] timeslots = new TextMeshProUGUI[3];

    [SerializeField, Required]
    XPLevelUI energy;

    [SerializeField, Required]
    XPLevelUI stress;

    [SerializeField, Required]
    XPLevelUI intelligence;

    [SerializeField, Required]
    TextButtonUI testButton;

    private void OnValidate()
    {
        Required.Assert(this);
    }

    private void Start()
    {
        testButton.OnClicked = () =>
        {
            GameManager.Instance.FinishExecute();
        };
    }

    private void Update()
    {
        SetUI();
    }

    /// <summary>
    /// Set UI according to game state
    /// </summary>
    public void SetUI()
    {
        GameState state = GameManager.Instance.GameStateManager.CurrentGameState;

        gamedateText.text = $"{state.CurrentDate.month:00}-{state.CurrentDate.day:00}";

        Timeslot[] slotsToday = state.Timeslots.Col(state.DaysPlayed);
        for (int i = 0; i < slotsToday.Length; i++)
        {
            timeslots[i].text = slotsToday[i].ToString();
            if (i < (int)state.CurrentTime)
            {
                timeslots[i].color = Color.gray;
            }
            else if (i == (int)state.CurrentTime)
            {
                timeslots[i].color = Color.red;
            }
            else
            {
                timeslots[i].color = Color.black;
            }
        }

        energy.SetUI("Energy", state.Energy, 0.5f);
        stress.SetUI("Stress", state.Stress, 0.5f);
        intelligence.SetUI("Intelligence", state.Intelligence);
    }
}
