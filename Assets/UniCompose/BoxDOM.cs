using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DOM
{
    // Not null. Empty array if no children.
    public DOM[] children = new DOM[0];

    protected abstract record BaseEquality;

    protected BaseEquality equality;

    // 1. Implement Constructor
    //    Set props and states. Do not initialize children.
    //    Initialize equality.
    // 2. Implement Compose

    public delegate IEnumerator<DOM> Composable(DOM dom);

    /// <summary>
    /// Compute direct children DOM using current state.
    /// </summary>
    public abstract DOM[] Compose();

    public override bool Equals(object obj)
    {
        return obj is DOM dom && dom.GetType() == GetType() && dom.equality == equality;
    }

    public override int GetHashCode()
    {
        return equality.GetHashCode();
    }

    /// <summary>
    /// Good way to read value from state in DOM object.
    /// </summary>
    public T Read<T>(MutableState<T> state)
    {
        return state.Get(this);
    }
}

public class RootDOM : DOM
{
    #region Exposed
    public AppDOM App => children[0] as AppDOM;
    #endregion

    #region Props and States
    #endregion

    record Equality : BaseEquality;

    public RootDOM()
    {
        equality = new Equality();
    }

    public override DOM[] Compose()
    {
        return new DOM[1] { new AppDOM() };
    }
}

public class AppDOM : DOM
{
    // Exposed
    public BoxDOM Content => children[0] as BoxDOM;

    record Equality : BaseEquality;

    public AppDOM()
    {
        equality = new Equality();
    }

    public override DOM[] Compose()
    {
        return new DOM[1] { new BoxDOM() };
    }
}

public class BoxDOM : DOM
{
    // Exposed
    public TTDOM TT => children[0] as TTDOM;

    // State
    public readonly MutableState<int> state; // TODO: make private

    record Equality(MutableState<int> State) : BaseEquality;

    public BoxDOM()
    {
        state = new MutableState<int>(5);
        equality = new Equality(state);
    }

    public override DOM[] Compose()
    {
        return new DOM[2] { new TTDOM(state), new TextDOM("bye"), };
    }
}

public class BBDOM : DOM
{
    public Composable content;

    public override DOM[] Compose()
    {
        throw new NotImplementedException();
    }
}

public class TTDOM : DOM
{
    // Exposed
    public TextDOM Text => children[0] as TextDOM;

    // State
    private readonly MutableState<int> state;

    record Equality(MutableState<int> State) : BaseEquality;

    public TTDOM(MutableState<int> state)
    {
        this.state = state;
        equality = new Equality(state);
    }

    public override DOM[] Compose()
    {
        var value = Read(state);
        return new DOM[1] { new TextDOM(value.ToString()) };
    }
}

public class TextDOM : DOM
{
    // State
    public readonly string text; // TODO: make private

    record Equality(string Text) : BaseEquality;

    public TextDOM(string text)
    {
        this.text = text;
        equality = new Equality(text);
    }

    public override DOM[] Compose()
    {
        return new DOM[0] { };
    }
}

public class MutableState<T>
{
    private T _value;
    private readonly HashSet<DOM> readers = new();

    public MutableState(T value)
    {
        _value = value;
    }

    /// <summary>
    /// Get value and subscribe if not already subscribed.
    /// </summary>
    public T Get(DOM dom)
    {
        readers.Add(dom);
        return _value;
    }

    /// <summary>
    /// Set value and recompose readers.
    /// </summary>
    /// <param name="value"></param>
    public void Set(T value)
    {
        if (_value.Equals(value))
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
            if (i >= prev_children.Length || !(new_children[i] == (prev_children[i])))
            {
                Recompose(new_children[i]);
                dom.children[i] = new_children[i];
            }
            else
            {
                dom.children[i] = prev_children[i];
            }
        }
        // TODO: discard
    }
}
