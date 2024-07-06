using System.Collections.Generic;
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

    public void Sample()
    {
        var dom = new BBDOM
        {
            content = (DOM dom) =>
            {
                IEnumerator<DOM> ContentGenerator()
                {
                    var state = new MutableState<int>(5);
                    var value = dom.Read(state);
                    yield return new TTDOM(state);

                    for (int i = 0; i < 5; i++)
                    {
                        yield return new TTDOM(new MutableState<int>(i));
                    }
                }

                return ContentGenerator();
            }
        };
    }
}
