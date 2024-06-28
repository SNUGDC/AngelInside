using UnityEditor;
using UnityEngine;

public class RequiredFieldValidator : MonoBehaviour
{
    [MenuItem("Custom/Validate Required Field in Loaded Scene")]
    static void ValidateScenes()
    {
        // Find all MonoBehaviours (include inactive) in Loaded Scenes
        foreach (MonoBehaviour script in Resources.FindObjectsOfTypeAll<MonoBehaviour>())
        {
            (bool isValid, string invalidField) = Required.ValidateScript(script);
            if (!isValid)
            {
                Debug.LogError(
                    $"Required {script.name}:{invalidField}",
                    context: script.gameObject
                );
            }
        }

        Debug.Log("Required Validation complete.");
    }
}
