using UnityEditor;
using StateTracker.StateMachine;

namespace StateTracker.Editor
{
    class StateMachineWindow : EditorWindow
    {
        Tracker target;

        [MenuItem("StateTracker/State Machine View")]
        public static void ShowWindow()
        {
            GetWindow(typeof(StateMachineWindow));
        }

        void OnGUI()
        {
            target = EditorGUILayout.ObjectField(target, typeof(Tracker), true) as Tracker;
            if (!target)
            {
                return;
            }
            foreach (var state in target.states)
            {
                foreach (var transition in state.transitions)
                {
                    EditorGUILayout.LabelField(state.name + "->" + transition.destinationName);
                }
            }
        }
    }
}