using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class XPLevelUI : MonoBehaviour
{
    [SerializeField, Required]
    TextMeshProUGUI labelText;

    [SerializeField, Required]
    TextMeshProUGUI levelText;
    Slider progressBar;

    private void OnValidate()
    {
        Assert.AreEqual(GetComponentsInChildren<Slider>().Length, 1);
    }

    private void Awake()
    {
        progressBar = GetComponentInChildren<Slider>();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="label"></param>
    /// <param name="level"></param>
    /// <param name="fill">value from 0 to 1</param>
    public void SetUI(string label, int level, float fill)
    {
        labelText.text = label;
        levelText.text = level.ToString();
        progressBar.value = fill;
    }

    public void SetUI(string label, XPLevel xpLevel)
    {
        SetUI(label, xpLevel.Level, xpLevel.Progress);
    }
}
