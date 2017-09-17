using UnityEngine;
using UnityEditor;
using StateTracker.StateMachine;
using System.Collections.Generic;
using System;

namespace StateTracker.Editor
{
    class StateMachineWindow : EditorWindow
    {
        Tracker target;
        List<StateFrame> frames = new List<StateFrame>();

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
            
            BeginWindows();
            foreach (var state in target.states)
            {
                StateFrame frame = GetFrame(state);
                frame.OnGUI();
                foreach (var transition in state.transitions)
                {
                    EditorGUILayout.LabelField(state.name + "->" + transition.destinationName);
                    frame.DrawLine(GetFrame(transition.destinationName));
                }
            }
            EndWindows();
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