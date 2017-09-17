using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StateTracker.Editor
{
    class WindowArea
    {
        private Rect _rect, _view;
        private EditorWindow _parent;
        private Vector2 _position;

        public WindowArea(Rect rect, EditorWindow parent)
        {
            _rect = rect;
            _parent = parent;
            _view = new Rect(rect.position, rect.size * 0.9f);
        }

        public void OnGUI(IEnumerable<IRectBasedGUI> targets)
        {
            _rect = GUILayoutUtility.GetRect(128, 1024, 128, 1024);

            _position = GUI.BeginScrollView(_rect, _position, _view);
            {
                DrawGrid();
                _parent.BeginWindows();
                {
                    foreach (IRectBasedGUI target in targets)
                    {
                        target.OnGUI(_rect);
                    }
                }
                _parent.EndWindows();
            }

            GUI.EndScrollView();
        }

        public void Begin()
        {
        }

        public void End()
        {
        }

        void DrawGrid()
        {
            if (Event.current.type != EventType.Repaint)
            {
                return;
            }

            var linemat = (Material)EditorGUIUtility.LoadRequired("SceneView/2DHandleLines.mat");
            linemat.SetPass(0);

            GL.PushMatrix();
            GL.Begin(GL.LINES);

            // Draws grid lines
            var minX = _position.x + _rect.x;
            var minY = _position.y + _rect.y;
            var pos = _parent.position;
            var maxX = minX + pos.width;
            var maxY = minY + pos.height;

            Color thickColor = Color.cyan;
            Color thinColor = Color.blue;
            DrawGridLines(12f, thickColor, new Vector2(minX, minY), new Vector2(maxX, maxY));
            DrawGridLines(120f, thinColor, new Vector2(minX, minY), new Vector2(maxX, maxY));

            GL.End();
            GL.PopMatrix();
        }

        void DrawGridLines(float gridSize, Color gridColor, Vector2 min, Vector2 max)
        {
            GL.Color(gridColor);

            // Vertical lines
            for (float num = min.x - (min.x % gridSize); num < max.x; num += gridSize)
            {
                GL.Vertex(new Vector2(num, min.y));
                GL.Vertex(new Vector2(num, max.y));
            }
            // Horizontal lines
            for (float num2 = min.y - (min.y % gridSize); num2 < max.y; num2 += gridSize)
            {
                GL.Vertex(new Vector2(min.x, num2));
                GL.Vertex(new Vector2(max.x, num2));
            }
        }
    }
}