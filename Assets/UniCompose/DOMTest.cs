using UnityEngine;

public class DOMTest : MonoBehaviour
{
    void Start()
    {
        var app = new AppDOM();
        var box = app.Content;
        var tt = box.TT;
        var text = tt.Text;

        Debug.Log($"value: {box.state._value}");
        Debug.Log($"text: {box.TT.Text.text}");

        Debug.Log("Setting state to 5...");
        box.state.Set(5);
        Debug.Log($"value: {box.state._value}");
        Debug.Log($"text: {box.TT.Text.text}");
    }
}
