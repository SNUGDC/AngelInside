using UnityEngine;

public class DOMTest : MonoBehaviour
{
    void Start()
    {
        var root = new RootDOM();
        Recomposer.Recompose(root);

        var app = root.App;
        var box = app.Content;
        var tt = box.TT;
        var text = tt.Text;

        Debug.Log($"text: {box.TT.Text.text}");

        Debug.Log("Setting state to 20...");
        box.state.Set(20);
        Debug.Log($"text: {box.TT.Text.text}");
    }
}
