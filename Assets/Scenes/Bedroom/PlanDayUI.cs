using System;
using SerializedTuples;
using SerializedTuples.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class PlanDayUI : MonoBehaviour
{
    // Exposed
    public string Title
    {
        set => weekdayText.text = value;
    }
    public Action<GameTime> OnTimeslotClicked
    {
        set
        {
            for (int i = 0; i < timeslotButtons.Length; i++)
            {
                var localI = i;
                timeslotButtons[i].v2.OnClicked = (() => value(timeslotButtons[localI].v1));
            }
        }
    }

    public void SetTimeslot(GameTime time, Timeslot timeslot)
    {
        for (int i = 0; i < timeslotButtons.Length; i++)
        {
            if (timeslotButtons[i].v1 == time)
            {
                timeslotButtons[i].v2.Text = timeslot.ToString();
                return;
            }
        }
    }

    public void SetTimeslots(Timeslot[] timeslots)
    {
        Assert.AreEqual(GameConstants.TimeslotsPerDay, timeslots.Length);
        for (int i = 0; i < timeslots.Length; i++)
        {
            SetTimeslot((GameTime)i, timeslots[i]);
        }
    }

    // Internal UI Fields
    [SerializeField, Required]
    private TextMeshProUGUI weekdayText;

    // TODO: required
    [SerializeField]
    [SerializedTupleLabels("Time", "Button")]
    private SerializedTuple<GameTime, TextButtonUI>[] timeslotButtons = new SerializedTuple<
        GameTime,
        TextButtonUI
    >[]
    {
        new(GameTime.Morning, null),
        new(GameTime.Afternoon, null),
        new(GameTime.Evening, null),
    };

    private void OnValidate()
    {
        Required.Assert(this);
        Assert.AreEqual(GameConstants.TimeslotsPerDay, timeslotButtons.Length);
        for (int i = 0; i < timeslotButtons.Length; i++)
        {
            for (int j = i + 1; j < timeslotButtons.Length; j++)
            {
                Assert.AreNotEqual(timeslotButtons[i].v1, timeslotButtons[j].v1);
            }
        }
    }
}
