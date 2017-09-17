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
        static int nextId = 0;
        int _id;
        private Vector2 size = new Vector2(150,50);
        private Rect _windowRect;

        public State State
        {
            get;
            private set;
        }

        public Rect rect
        {
            get
            {
                return _windowRect;
            }
        }

        public StateFrame(State state)
        {
            _id = nextId++;
            State = state;

            _windowRect = new Rect(state.position, size);
        }

        public void OnGUI(Rect rect)
        {
            _windowRect = GUILayout.Window(_id, _windowRect, OnWindowDraw, State.name);
        }

        private void OnWindowDraw(int id)
        {
            GUILayout.Label("Transitions: " + State.transitions.Count);

            GUI.DragWindow();
        }

        internal void DrawLine(StateFrame other)
        {
            Handles.DrawLine(_windowRect.center, other._windowRect.center);
        }
    }
}
