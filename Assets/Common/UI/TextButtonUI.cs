using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TextButtonUI : MonoBehaviour
{
    // Exposed
    public string Text
    {
        get { return text; }
        set
        {
            text = value;
            tmp.text = text;
        }
    }

    /// <summary>
    /// Only one listener can be set at a time
    /// </summary>
    public Action OnClicked
    {
        set
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => value.Invoke());
        }
    }

    // UI Part
    private TextMeshProUGUI tmp;
    private Button button;

    [SerializeField]
    private string text;

    private void OnValidate()
    {
        Assert.AreEqual(GetComponentsInChildren<TextMeshProUGUI>().Length, 1);
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    // TODO: Instantiate from GameAssetReferences could be automated?
    //       By setting attribute on the class, GameAssetReferences extend
    public static TextButtonUI Instantiate(Transform parent, string text, Action onClicked)
    {
        GameObject obj = GameAssetReferences.TextButton.InstantiateSync(parent);
        TextButtonUI textButtonUI = obj.GetComponent<TextButtonUI>();
        textButtonUI.Text = text;
        textButtonUI.OnClicked = onClicked;
        return textButtonUI;
    }
}
