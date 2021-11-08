using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Project.Colors.Editor
{
    [CustomPropertyDrawer(typeof(ColorInPalette))]
    public class ColorInPaletteDrawer : PropertyDrawer
    {
        BindingFlags _flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        FieldInfo[] _fields;
        string[] _fieldNames;
        int[] _fieldIndices;

        SerializedProperty _colorName;
        SerializedProperty _color;



        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _fields = typeof(ColorLibrary).GetFields(_flags);
            _fieldNames = new string[_fields.Length];
            _fieldIndices = new int[_fields.Length];
            for (int i = 0; i < _fields.Length; i++)
            {
                _fieldNames[i] = _fields[i].Name;
                _fieldIndices[i] = i;
            }

            _colorName = property.FindPropertyRelative("ColorName");
            _color = property.FindPropertyRelative("Color");

            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            _colorName.intValue = EditorGUI.IntPopup(position, _colorName.intValue, _fieldNames, _fieldIndices);
            _color.colorValue = (Color32)_fields[_colorName.intValue].GetValue(property);

            GUI.enabled = false;
            Rect colorPosition = new Rect(position.x, position.y + 20, position.width, 20);
            EditorGUI.PropertyField(colorPosition, _color, GUIContent.none);
            GUI.enabled = true;

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 40;
        }
    }
}