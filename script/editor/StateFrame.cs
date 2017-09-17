using StateTracker.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace StateTracker.Editor
{
    class StateFrame : IRectBasedGUI
    {
        public Rect windowRect;
        static int nextId = 0;
        int _id;
        private Vector2 size = new Vector2(150,50);

        public State State
        {
            get;
            private set;
        }

        public StateFrame(State state)
        {
            _id = nextId++;
            State = state;

            windowRect = new Rect(state.position, size);
        }

        public void OnGUI(Rect rect)
        {
            windowRect = GUILayout.Window(_id, windowRect, OnWindowDraw, State.name);
            State.position = windowRect.position;
        }

        private void OnWindowDraw(int id)
        {
            GUILayout.Label("Transitions: " + State.transitions.Count);

            GUI.DragWindow();
        }

        internal void DrawLine(StateFrame other)
        {
            Handles.DrawLine(windowRect.center, other.windowRect.center);
        }
    }
}
