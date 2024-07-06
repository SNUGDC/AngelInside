using System;
using SerializedTuples;
using SerializedTuples.Runtime;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlanPanelUI : MonoBehaviour
{
    // Exposed
    public Action<Timeslot[,]> OnPlanFinished { get; set; }

    // State
    /// <summary>
    /// timeslots[time, weekday]
    /// </summary>
    /// so row x col can look like PlanPanelUI
    public Timeslot[,] timeslots;

    // Internal UI State
    int selectedWeekdayIndex;
    GameTime selectedTime;

    // Internal UI Fields
    // TODO: required
    [SerializeField]
    [SerializedTupleLabels("weekday", "planDayUI")]
    SerializedTuple<GameWeekday, PlanDayUI>[] days = new SerializedTuple<GameWeekday, PlanDayUI>[7]
    {
        new(GameWeekday.MON, null),
        new(GameWeekday.TUE, null),
        new(GameWeekday.WED, null),
        new(GameWeekday.THU, null),
        new(GameWeekday.FRI, null),
        new(GameWeekday.SAT, null),
        new(GameWeekday.SUN, null),
    };

    [SerializeField, Required]
    Button checkButton;

    private BackpanelUI contextMenu; // BackpanelUI is wrapping ContextMenuUI
    private ContextMenuUI contextMenuUI;

    private void OnValidate()
    {
        Required.Assert(this);
        Assert.AreEqual(GameConstants.DaysPerWeek, days.Length);
    }

    private void Awake()
    {
        timeslots = new Timeslot[GameConstants.TimeslotsPerDay, GameConstants.DaysPerWeek];

        checkButton.onClick.RemoveAllListeners();
        checkButton.onClick.AddListener(OnCheckButtonClicked);

        // ContextMenu
        var menuItems = new (string, Action)[Enum.GetNames(typeof(Timeslot)).Length];
        for (int i = 0; i < menuItems.Length; i++)
        {
            var localSlot = (Timeslot)i;
            menuItems[i] = (localSlot.ToString(), () => OnContextMenuItemClicked(localSlot));
        }
        contextMenuUI = ContextMenuUI.Instantiate(parent: transform, menuItems);

        contextMenu = BackpanelUI.Instantiate(
            parent: transform,
            content: contextMenuUI.gameObject,
            onClicked: () =>
            {
                contextMenu.Active = false;
            },
            mode: BackpanelUI.Mode.FullScreen,
            dimmed: false
        );

        contextMenu.Active = false;
    }

    private void Start()
    {
        for (int i = 0; i < days.Length; i++)
        {
            (GameWeekday weekday, PlanDayUI planDayUI) = days[i];
            planDayUI.Title = weekday.ToString();
            planDayUI.SetTimeslots(timeslots.Col(i));
            planDayUI.OnTimeslotClicked = (GameTime time) =>
            {
                OnTimeslotButtonClicked(weekday, time);
            };
        }
    }

    private void OnTimeslotButtonClicked(GameWeekday weekday, GameTime time)
    {
        selectedWeekdayIndex = -1;
        for (int i = 0; i < days.Length; i++)
        {
            if (days[i].v1 == weekday)
            {
                selectedWeekdayIndex = i;
                break;
            }
        }
        selectedTime = time;

        // Show context menu at Mouse Position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform, // TODO: maybe canvas.transform?
            Input.mousePosition,
            null, // For a RectTransform in a Canvas set to Screen Space - Overlay mode, the cam parameter should be null.
            out Vector2 localPos
        );
        contextMenuUI.Position = localPos; // Pivot is bottom left of contextMenu
        contextMenu.Active = true;
    }

    private void OnContextMenuItemClicked(Timeslot timeslot)
    {
        contextMenu.Active = false;
        timeslots[(int)selectedTime, selectedWeekdayIndex] = timeslot;
        days[selectedWeekdayIndex].v2.SetTimeslot(selectedTime, timeslot);
    }

    private void OnCheckButtonClicked()
    {
        OnPlanFinished(timeslots);
    }
}

static class ArrayExtension
{
    public static int RowLength<T>(this T[,] array)
    {
        return array.GetLength(0);
    }

    public static int ColLength<T>(this T[,] array)
    {
        return array.GetLength(1);
    }

    public static T[] Row<T>(this T[,] array, int row)
    {
        int columns = array.ColLength();
        T[] result = new T[columns];
        for (int i = 0; i < columns; i++)
        {
            result[i] = array[row, i];
        }
        return result;
    }

    public static T[] Col<T>(this T[,] array, int col)
    {
        int rows = array.RowLength();
        T[] result = new T[rows];
        for (int i = 0; i < rows; i++)
        {
            result[i] = array[i, col];
        }
        return result;
    }
}
