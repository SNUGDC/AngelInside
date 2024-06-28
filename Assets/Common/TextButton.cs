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

    [SerializeField]
    Button.ButtonClickedEvent onClicked;

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
        button.onClick = onClicked;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
}
