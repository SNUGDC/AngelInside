using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ContextMenuUI : MonoBehaviour
{
    public Vector2 Position
    {
        set { GetComponent<RectTransform>().anchoredPosition = value; }
    }

    public void AddItem(string text, Action action)
    {
        TextButtonUI.Instantiate(transform, text: text, onClicked: action);
    }

    public void ClearItems()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public static ContextMenuUI Instantiate(Transform parent, (string, Action)[] menuItems)
    {
        GameObject obj = GameAssetReferences.ContextMenu.InstantiateSync(parent);
        ContextMenuUI contextMenuUI = obj.GetComponent<ContextMenuUI>();

        if (menuItems != null)
        {
            contextMenuUI.ClearItems();
            for (int i = 0; i < menuItems.Length; i++)
            {
                (string text, Action action) = menuItems[i];
                contextMenuUI.AddItem(text, action);
            }
        }

        return contextMenuUI;
    }
}
