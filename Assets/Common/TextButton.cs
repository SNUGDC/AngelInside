using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TextButton : MonoBehaviour
{
    // UI Part
    private Button button;
    private TextMeshProUGUI text;

    private void OnValidate()
    {
        Assert.AreEqual(GetComponentsInChildren<TextMeshProUGUI>().Length, 1);
    }

    public string Text
    {
        get { return text.text; }
        set { text.text = value; }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void AddListener(UnityEngine.Events.UnityAction call)
    {
        button.onClick.AddListener(call);
    }
}
