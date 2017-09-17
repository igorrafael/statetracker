using UnityEngine;
using UnityEditor;
using System.Collections;

namespace StateTracker {
    class StateMachineWindow : EditorWindow
    {
        StateMachine target;

        [MenuItem("StateTracker/State Machine View")]
        public static void ShowWindow()
        {
            GetWindow(typeof(StateMachineWindow));
        }

        void OnGUI()
        {
            target = EditorGUILayout.ObjectField(target, typeof(StateMachine), true) as StateMachine;
            if (!target)
            {
                return;
            }
            foreach (var state in target.States)
            {
                EditorGUILayout.LabelField(state.name);
            }
        }
    }
}