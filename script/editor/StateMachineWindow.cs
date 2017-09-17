using UnityEngine;
using UnityEditor;
using StateTracker.StateMachine;
using System.Collections.Generic;
using System.Linq;

namespace StateTracker.Editor
{
    class StateMachineWindow : EditorWindow
    {
        Tracker target;
        List<StateFrame> frames = new List<StateFrame>();
        private WindowArea windowArea;

        [MenuItem("StateTracker/State Machine View")]
        public static void ShowWindow()
        {
            GetWindow(typeof(StateMachineWindow));
        }

        void OnGUI()
        {
            Rect windowRect = new Rect
            {
                size = position.size * 0.95f
            };
            windowArea = new WindowArea(windowRect, this);

            target = EditorGUILayout.ObjectField(target, typeof(Tracker), true) as Tracker;
            target = target ?? Resources.FindObjectsOfTypeAll<Tracker>().First();
            if (!target)
            {
                return;
            }

            foreach (var state in target.states)
            {
                foreach (var transition in state.transitions)
                {
                    EditorGUILayout.LabelField(state.name + "->" + transition.destinationName);
                    GetFrame(state).DrawLine(GetFrame(transition.destinationName));
                }
            }

            windowArea.Begin();
            target.states.ForEach(s => GetFrame(s).OnGUI());
            windowArea.End();
        }

        private StateFrame GetFrame(string destinationName)
        {
            State state = target.states.Find(s => s.name == destinationName);
            //TODO warn missing state
            return GetFrame(state);
        }

        private StateFrame GetFrame(State state)
        {
            StateFrame frame = frames.Find(f => f.State == state);
            if (frame == null)
            {
                frame = new StateFrame(state);
                frames.Add(frame);
            }

            return frame;
        }
    }
}