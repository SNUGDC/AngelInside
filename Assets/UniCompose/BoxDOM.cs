using System.Collections.Generic;
using UnityEngine;

public abstract class DOM
{
    // Not null. Empty array if no children.
    public DOM[] children = new DOM[0];

    // 1. Implement Constructor
    //    Set props and states. Do not initialize children.
    // 2. Implement Compose

    /// <summary>
    /// Compute direct children DOM using current state.
    /// </summary>
    public abstract DOM[] Compose();

    public abstract bool DOMEquals(DOM dom);
}

public class RootDOM : DOM
{
    // Exposed
    public AppDOM App => children[0] as AppDOM;

    public override DOM[] Compose()
    {
        return new DOM[1] { new AppDOM() };
    }

    public override bool DOMEquals(DOM dom)
    {
        return dom is RootDOM;
    }
}

public class AppDOM : DOM
{
    // Exposed
    public BoxDOM Content => children[0] as BoxDOM;

    public AppDOM() { }

    public override DOM[] Compose()
    {
        return new DOM[1] { new BoxDOM() };
    }

    public override bool DOMEquals(DOM dom)
    {
        return dom is AppDOM;
    }
}

public class BoxDOM : DOM
{
    // Exposed
    public TTDOM TT => children[0] as TTDOM;

    // State
    public readonly MutableState state; // TODO: make private

    public BoxDOM()
    {
        state = new MutableState();
    }

    public override DOM[] Compose()
    {
        return new DOM[2] { new TTDOM(state), new TextDOM("bye"), };
    }

    public override bool DOMEquals(DOM dom)
    {
        return dom is BoxDOM boxdom && boxdom.state == state;
    }
}

public class TTDOM : DOM
{
    public TextDOM Text => children[0] as TextDOM;

    // State
    private readonly MutableState state;

    public TTDOM(MutableState state)
    {
        this.state = state;
    }

    public override DOM[] Compose()
    {
        var value = state.Get(this);
        return new DOM[1] { new TextDOM(value.ToString()) };
    }

    public override bool DOMEquals(DOM dom)
    {
        return dom is TTDOM ttdom && ttdom.state == state;
    }
}

public class TextDOM : DOM
{
    // State
    public string text;

    public TextDOM(string text)
    {
        this.text = text;
    }

    public override DOM[] Compose()
    {
        return new DOM[0] { };
    }

    public override bool DOMEquals(DOM dom)
    {
        return dom is TextDOM textdom && textdom.text == text;
    }
}

public class MutableState
{
    private int _value = 0;
    private readonly HashSet<DOM> readers = new();

    /// <summary>
    /// Get value and subscribe if not already subscribed.
    /// </summary>
    public int Get(DOM dom)
    {
        readers.Add(dom);
        return _value;
    }

    public void Set(int value)
    {
        if (_value == value)
            return;
        _value = value;
        foreach (var reader in readers)
        {
            Recomposer.Recompose(reader);
        }
    }
}

public class Recomposer : MonoBehaviour
{
    public static void Recompose(DOM dom)
    {
        Debug.Log($"Recomposing {dom.GetType().Name}");
        DOM[] prev_children = dom.children;
        DOM[] new_children = dom.Compose();

        dom.children = new DOM[new_children.Length];
        for (int i = 0; i < new_children.Length; i++)
        {
            // TODO: use key to match children (reduce recomposition)
            if (i >= prev_children.Length || !new_children[i].DOMEquals(prev_children[i]))
            {
                Recompose(new_children[i]);
                dom.children[i] = new_children[i];
            }
            else
            {
                dom.children[i] = prev_children[i];
            }
        }
    }
}
