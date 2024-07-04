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

    private void OnValidate()
    {
        Required.Assert(this);
    }

    /// <summary>
    /// Set UI according to game state
    /// </summary>
    public void SetUI()
    {
        GameState state = GameStateManager.CurrentGameState;

        gamedateText.text = $"{state.CurrentDate.month:00}-{state.CurrentDate.day:00}";

        for (int i = 0; i < GameConstants.TimeslotsPerDay; i++)
        {
            Timeslot timeslot = state.Timeslots[state.DaysPlayed, i];
            timeslots[i].text = timeslot.ToString();
        }

        energy.SetUI("Energy", state.Energy, 0.5f);
        stress.SetUI("Stress", state.Stress, 0.5f);
        intelligence.SetUI("Intelligence", state.Intelligence);
    }
}
