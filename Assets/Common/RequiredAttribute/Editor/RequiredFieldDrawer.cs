using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Required))]
public class RequiredFieldDrawer : PropertyDrawer
{
    float OriginalPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

    const int warningHeight = 24;

    static bool CheckIfNull(SerializedProperty property)
    {
        return property.objectReferenceValue == null;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect objectPosition = position;
        objectPosition.height = OriginalPropertyHeight(property, label);
        EditorGUI.ObjectField(objectPosition, property, label);

        if (CheckIfNull(property))
        {
            Rect warningPosition = EditorGUI.IndentedRect(position);
            warningPosition.y += OriginalPropertyHeight(property, label);
            warningPosition.height = warningHeight;
            EditorGUI.HelpBox(warningPosition, "Object is null", MessageType.Warning);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (CheckIfNull(property))
        {
            return OriginalPropertyHeight(property, label) + warningHeight;
        }
        return OriginalPropertyHeight(property, label);
    }
}
