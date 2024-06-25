using System.Reflection;
using UnityEditor;
using UnityEngine;

public class RequiredFieldValidator : MonoBehaviour
{
    [MenuItem("Custom/Validate Required Field")]
    static void Validate()
    {
        // Find all MonoBehaviours (include inactive) in Loaded Scenes
        foreach (MonoBehaviour script in Resources.FindObjectsOfTypeAll<MonoBehaviour>())
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
                    // NOTE: GetValue is returning "null" string instead of null object
                    //       t may not be GameObject (t could be Component)
                    if (t as GameObject == null && t as Component == null)
                    {
                        Debug.LogError(
                            $"{field.Name} is not assigned in {script.name}.",
                            context: script.gameObject
                        );
                    }
                }
            }
        }

        Debug.Log("Validation complete.");
    }
}
