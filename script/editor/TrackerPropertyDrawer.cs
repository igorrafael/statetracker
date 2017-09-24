using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Collections.Generic;

namespace StateTracker.StateMachine
{
    [CustomPropertyDrawer(typeof(Tracker))]
    public class TrackerDrawer : PropertyDrawer
    {
        float lineHeight = 20f;
        float buttonWidth = 60f;
        private Vector2 _buttonScrollPosition;
        private List<State> _states;

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

            string[] values = GetMissingValues(property);
            Rect scrollRect = position;
            scrollRect.height = lineHeight * 2;
            var viewRect = scrollRect;
            viewRect.position -= _buttonScrollPosition;
            viewRect.width = buttonWidth * values.Length;
            _buttonScrollPosition = GUI.BeginScrollView(scrollRect, _buttonScrollPosition, viewRect, true, false);
            {
                var buttonPosition = scrollRect;
                buttonPosition.position -= _buttonScrollPosition;
                buttonPosition.height = lineHeight;
                buttonPosition.width = buttonWidth;
                {
                    foreach (var value in values)
                    {
                        if (GUI.Button(buttonPosition, "+" + value))
                        {
                            var state = new State()
                            {
                                name = value.ToString()
                            };
                            tracker.states.Add(state);
                        }
                        buttonPosition.x += buttonPosition.width;
                    }
                }
            }
            GUI.EndScrollView();
            scrollRect.position += new Vector2(0, scrollRect.height);

            var labelRect = scrollRect;
            labelRect.height = lineHeight;
            foreach (var state in _states)
            {
                GUI.Label(labelRect, state.name);
                labelRect.position += new Vector2(0, labelRect.height);
                foreach (var t in state.transitions)
                {
                    //TODO
                }
            }

            EditorGUI.EndProperty();
        }

        private void UpdateStates(SerializedProperty property)
        {
            var tracker = property.objectReferenceValue as Tracker;
            _states = tracker.states;
        }

        private string[] GetMissingValues(SerializedProperty property)
        {
            var trackabletype = fieldInfo.DeclaringType.GetGenericArguments()[0];
            var values = Enum.GetNames(trackabletype);
            var tracker = property.objectReferenceValue as Tracker;
            return values.Where(v => tracker.states.All(s => s.name != v)).ToArray();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //Scroll rect with "add state"
            float height = lineHeight * 2f;

            UpdateStates(property);
            //space for each state
            foreach (var state in _states)
            {
                height += lineHeight;
            }
            Debug.Log(height);

            return height;
        }
    }
}