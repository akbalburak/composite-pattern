#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Wave.Engine.Composite.Attributes
{
    public class ReadOnlyAttribute : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false; // Disable editing for read-only fields
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true; // Re-enable editing for other fields
        }
    }
}
