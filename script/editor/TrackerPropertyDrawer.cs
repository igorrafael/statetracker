using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

namespace StateTracker.StateMachine
{
    [CustomPropertyDrawer(typeof(Tracker))]
    public class TrackerDrawer : PropertyDrawer
    {
        float lineHeight = 20f;
        private bool _showButtons;

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            //int indent = EditorGUI.indentLevel;
            //EditorGUI.indentLevel = 0;

            // Calculate rects
            //Rect nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            //EditorGUI.PropertyField(position, property.FindPropertyRelative("test"), GUIContent.none);
            var tracker = property.objectReferenceValue as Tracker;
            //tracker.test = EditorGUI.TextField(position, tracker.test);
            property.objectReferenceValue = tracker;

            Rect buttonPosition = position;
            buttonPosition.height = lineHeight;
            _showButtons = EditorGUI.Foldout(buttonPosition, _showButtons, "Missing states");
            if (_showButtons)
            {
                Array values = GetMissingValues(property);
                foreach (var value in values)
                {
                    if (GUI.Button(buttonPosition, "Add " + value))
                    {
                        var state = new State()
                        {
                            name = value.ToString()
                        };
                        tracker.states.Add(state);
                    }
                    buttonPosition.y += buttonPosition.height;
                }
            }

            // Set indent back to what it was
            //EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        private string[] GetMissingValues(SerializedProperty property)
        {
            var trackabletype = fieldInfo.DeclaringType.GetGenericArguments()[0];
            var values = Enum.GetNames(trackabletype);
            var tracker = property.objectReferenceValue as Tracker;
            return values.Where(v=>tracker.states.All(s=>s.name!=v)).ToArray();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float buttons = _showButtons ? lineHeight : GetMissingValues(property).Length * lineHeight;


            return buttons;
        }
    }
}