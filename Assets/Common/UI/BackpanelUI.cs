using System;
using UnityEngine;
using UnityEngine.UI;

// NOTE: Generic MonoBehaviour is not supporectTransformed
[RequireComponent(typeof(RectTransform), typeof(Button))]
public class BackpanelUI : MonoBehaviour
{
    [SerializeField]
    private Mode mode;

    public bool Active
    {
        set { gameObject.SetActive(value); }
    }

    public T GetContent<T>()
        where T : Component
    {
        return transform.GetChild(0).gameObject.GetComponent<T>();
    }

    public void SetContent<T>(T value)
        where T : Component
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        value.transform.SetParent(transform);
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

    public enum Mode
    {
        FullScreen,
        MatchParent,
        MatchCanvas
    }

    // UI ParectTransform
    private Button button;
    private RectTransform rectTransform;

    private void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        SetSize();
    }

    void SetSize()
    {
        switch (mode)
        {
            case Mode.FullScreen:
                var prevParent = transform.parent;

                // set the new parent to the canvas
                transform.SetParent(FindObjectOfType<Canvas>().transform, false);

                // fit it to screen
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.sizeDelta = Vector2.zero;
                rectTransform.transform.localPosition = Vector3.zero;

                // now set it back but using the true parameter to keep our scaling
                transform.SetParent(prevParent, true);
                break;
            case Mode.MatchParent:
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.sizeDelta = Vector2.zero;
                rectTransform.transform.localPosition = Vector3.zero;
                break;
            case Mode.MatchCanvas:

                throw new NotImplementedException();
            //break;
            default:
                throw new NotImplementedException();
        }
    }

    public static BackpanelUI Instantiate(
        Transform parent,
        GameObject content,
        Action onClicked,
        Mode mode,
        bool dimmed
    )
    {
        GameObject obj = new("Popup");
        obj.transform.SetParent(parent);
        obj.transform.SetAsLastSibling(); // Make sure it's on top of siblings

        content.transform.SetParent(obj.transform);

        BackpanelUI backpanelUI = obj.AddComponent<BackpanelUI>();
        backpanelUI.OnClicked = onClicked;

        backpanelUI.mode = mode;
        backpanelUI.SetSize();

        Image img = obj.AddComponent<Image>();
        img.color = dimmed ? new Color(0, 0, 0, 0.5f) : Color.clear;

        return backpanelUI;
    }
}
