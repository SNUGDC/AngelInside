using System.Collections.Generic;
using UnityEngine;

public abstract class DOM
{
    public DOM[] children;

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

    public AppDOM()
    {
        children = Compose();
    }

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
        children = Compose();
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
        children = Compose();
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
    public int _value = 0; // TODO: make private
    private readonly HashSet<DOM> readers = new();

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
        DOM[] new_children = dom.Compose();
        for (int i = 0; i < new_children.Length; i++)
        {
            if (i >= dom.children.Length || !new_children[i].DOMEquals(dom.children[i]))
            {
                Recompose(new_children[i]);
            }
        }
        dom.children = new_children;
    }
}
