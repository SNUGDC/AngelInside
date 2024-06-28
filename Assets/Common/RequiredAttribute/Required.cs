using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>This field should be set in the Inspector</summary>
[AttributeUsage(AttributeTargets.Field)]
public class Required : PropertyAttribute
{
    /// <returns>(bool isValid, string invalidField)</returns>
    public static (bool, string) ValidateScript(MonoBehaviour script)
    {
        foreach (
            FieldInfo field in script
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        )
        {
            if (field.GetCustomAttributes(typeof(Required), true).Length > 0)
            {
                var t = field.GetValue(script);
                if (t is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null)
                        {
                            return (false, field.Name);
                        }
                    }
                }
                else
                {
                    // NOTE: GetValue is returning "null" string instead of null object
                    if (t is null or (object)"null")
                    {
                        return (false, field.Name);
                    }
                }
            }
        }

        return (true, "");
    }

    public static void Assert(MonoBehaviour script)
    {
        (bool isValid, string invalidField) = ValidateScript(script);
        if (!isValid)
        {
            throw new AssertionException(
                message: "Required Failure",
                userMessage: $"Required {script.name}:{invalidField}"
            );
        }
    }
}
