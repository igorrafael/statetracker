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
        //TODO move this to another file
        static Texture2D _texture;
        static Texture2D Texture
        {
            get
            {
                //if (_texture == null)
                {
                    int height = 32;
                    _texture = new Texture2D(1, height, TextureFormat.ARGB4444, true);
                    float step = 1f / (height / 2 - 1);
                    for (int j = 0; j < height/2; j++)
                    {
                        float a = step * j;
                        _texture.SetPixel(1, j, new Color(1, 1, 1, a));
                        _texture.SetPixel(1, height-j, new Color(1, 1, 1, a));
                    }
                    _texture.Apply();
                    _texture.hideFlags = HideFlags.HideAndDontSave;
                }
                return _texture;
            }
        }

        static int nextId = 0;
        int _id;
        private Vector2 size = new Vector2(150, 50);
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
            Vector3 start = _windowRect.center;
            Vector3 end = other._windowRect.center;
            Vector3 tan = start.x < end.x ? Vector3.right : -Vector3.right;
            tan *= 100;
            Handles.DrawBezier(start, end, start + tan, end - tan, Color.white, Texture, 4f);
        }
    }
}
