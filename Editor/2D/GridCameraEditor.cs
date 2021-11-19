using UnityEngine;
using UnityEditor;
using BaseTool._2D;
using BaseTool.Generic.Extensions;
using System;

namespace BaseTool.Editor._2D
{
    [CustomEditor(typeof(GridCamera))]
    public class GridCameraEditor : UnityEditor.Editor
    {
        private GridCamera _reference;

        private Camera _camera;

        private int _selectedBounds = -1;

        private void OnEnable()
        {
            _reference = (GridCamera)target;
            if(_reference.TryGetComponent(out Camera camera))
            {
                _camera = camera;

                if (!_camera.orthographic)
                {
                    Debug.LogWarning(
                        $"{target.name} switched to orthographic projection because of the GridCamera.",
                        target
                    );
                }
                _camera.orthographic = true;
            }

            if(_reference.Bounds.Count == 0)
            {
                _reference.Bounds.Add(GetGameViewBounds());
            }
        }

        private void OnSceneGUI()
        {
            _reference = (GridCamera)target;
            if (!_reference.ShowGizmos) return;

            Vector2 gameView = Handles.GetMainGameViewSize();
            float screenAspect = gameView.x / gameView.y;
            float cameraHeight = _camera.orthographicSize * 2;
            Bounds bounds = new Bounds(
                _camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0)
            );

            Vector2 mousePos = Event.current.mousePosition;
            Vector3 pos = HandleUtility.GUIPointToWorldRay(mousePos).origin;
            pos.z = 0;
            //Handles.color = Color.red;
            //Handles.DrawWireCube(pos, bounds.size);
            if (!PointIsInBounds(pos))
            {
                Bounds nearestBounds = GetNearestBoundsFrom(pos);

                int horizontalSide = nearestBounds.center.x < pos.x ? 1 : -1;
                int verticalSide = nearestBounds.center.y < pos.y ? 1 : -1;
                Handles.color = Color.red;

                Vector3 createPos = new Vector3(
                    nearestBounds.center.x + (horizontalSide * (Mathf.Abs(nearestBounds.size.x / 2) + Mathf.Abs(bounds.size.x / 2))),
                    nearestBounds.center.y,
                    0
                );

                // Horizontal
                if (Mathf.Abs(pos.x) - Mathf.Abs(nearestBounds.center.x) > Mathf.Abs(pos.y) - Mathf.Abs(nearestBounds.center.y))
                {
                    createPos = new Vector3(
                       nearestBounds.center.x + (horizontalSide * (Mathf.Abs(nearestBounds.size.x / 2) + Mathf.Abs(bounds.size.x / 2))),
                       nearestBounds.center.y,
                       0
                   );
                }
                else // Vertical
                {
                    createPos = new Vector3(
                        nearestBounds.center.x,
                        nearestBounds.center.y + (verticalSide * (Mathf.Abs(nearestBounds.size.y / 2) + Mathf.Abs(bounds.size.y / 2))),
                        0
                    );
                }

                Handles.DrawWireCube(createPos, bounds.size);
                if (Handles.Button(createPos, Quaternion.identity, 1000, 1000, Handles.RectangleHandleCap))
                {
                    _reference.Bounds.Add(new Bounds(createPos, bounds.size));
                }
            }

            DisplayBounds(pos);

            DrawSelectedBounds();

            if(_selectedBounds != -1)
            {
                Handles.BeginGUI();

                if (GUI.Button(new Rect(0, 0, 100, 30), "Remove"))
                {
                    _reference.Bounds.RemoveAt(_selectedBounds);
                    _selectedBounds = -1;
                }
                Handles.EndGUI();
            }

            // Force scene redraw
            SceneView.RepaintAll();
        }

        private void DisplayBounds(Vector3 mousePos)
        {
            int i = 0;
            foreach (Bounds b in _reference.Bounds)
            {
                if(_selectedBounds != i)
                {
                    if (mousePos.x.IsBetween(b.min.x, b.max.x) && mousePos.y.IsBetween(b.min.y, b.max.y))
                    {
                        //Handles.color = new Color(0.25f, 1, 0.1f);
                        Rect pos = new Rect(b.min, b.size);
                        Handles.DrawSolidRectangleWithOutline(
                            pos,
                            new Color(0.25f, 1, 0.1f, 0.1f),
                            Color.green
                        );
                        Handles.Label(b.center, $"Zone {i}");
                        if (Handles.Button(b.center, Quaternion.identity, 1000, 1000, Handles.RectangleHandleCap))
                        {
                            _selectedBounds = i;
                        }
                    }
                    else
                    {
                        Handles.color = Color.green;
                        Handles.DrawWireCube(b.center, b.size);
                    }
                }

                ++i;
            }
        }

        private bool PointIsInBounds(Vector3 point)
        {
            foreach (Bounds b in _reference.Bounds)
            {
                if (point.x.IsBetween(b.min.x, b.max.x) && point.y.IsBetween(b.min.y, b.max.y))                   
                    return true;
            }
            return false;
        }

        private Bounds GetGameViewBounds()
        {
            Vector2 gameView = Handles.GetMainGameViewSize();
            float screenAspect = gameView.x / gameView.y;
            float cameraHeight = _camera.orthographicSize * 2;
            Bounds bounds = new Bounds(
                _camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }

        private Bounds GetNearestBoundsFrom(Vector3 pos)
        {
            Bounds nearest = default;
            foreach (Bounds b in _reference.Bounds)
            {
                if (
                    nearest == default ||
                    (Vector3.Distance(b.center, pos) < Vector3.Distance(nearest.center, pos))
                ) nearest = b;
            }

            return nearest;
        }

        private void DrawSelectedBounds()
        {
            if (_selectedBounds == -1) return;

            Bounds bounds = _reference.Bounds[_selectedBounds];
            Vector3 pos = bounds.center;
            Rect rect = new Rect(bounds.min, bounds.size);
            Rect newRect = ResizeRect(
                rect,
                Handles.CubeHandleCap,
                Color.green,
                Color.yellow,
                HandleUtility.GetHandleSize(Vector3.zero) * .1f,
                .1f
            );
            bounds.SetMinMax(newRect.min, newRect.max);
            Debug.Log($"{bounds.center}");
            _reference.Bounds[_selectedBounds] = bounds;            
        }

        public static Rect ResizeRect(Rect rect, Handles.CapFunction capFunc, Color capCol, Color fillCol, float capSize, float snap)
        {
            Vector2 halfRectSize = new Vector2(rect.size.x * 0.5f, rect.size.y * 0.5f);

            Vector3[] rectangleCorners =
                {
                new Vector3(rect.min.x, rect.min.y, 0),   // Bottom Left
                new Vector3(rect.max.x, rect.min.y, 0),   // Bottom Right
                new Vector3(rect.max.x, rect.max.y, 0),   // Top Right
                new Vector3(rect.min.x, rect.max.y, 0)    // Top Left
            };

            Handles.color = fillCol;
            Handles.DrawSolidRectangleWithOutline(rectangleCorners, new Color(fillCol.r, fillCol.g, fillCol.b, 0.25f), capCol);

            Vector3[] handlePoints =
                {
                new Vector3(rect.min.x, rect.center.y, 0),   // Left
                new Vector3(rect.max.x, rect.center.y, 0),   // Right
                new Vector3(rect.center.x, rect.max.y, 0),   // Top
                new Vector3(rect.center.x, rect.min.y, 0)    // Bottom 
            };

            Handles.color = capCol;

            var newSize = rect.size;
            var newPosition = rect.center;

            var leftHandle = Handles.Slider(handlePoints[0], Vector3.left, capSize, capFunc, snap).x - handlePoints[0].x;
            var rightHandle = Handles.Slider(handlePoints[1], Vector3.right, capSize, capFunc, snap).x - handlePoints[1].x;
            var topHandle = Handles.Slider(handlePoints[2], Vector3.up, capSize, capFunc, snap).y - handlePoints[2].y;
            var bottomHandle = Handles.Slider(handlePoints[3], Vector3.down, capSize, capFunc, snap).y - handlePoints[3].y;

            //if(leftHandle != 0 || rightHandle != 0 || topHandle != 0 || bottomHandle != 0)
            //    Debug.Log($"left({leftHandle}), right({rightHandle}), top({topHandle}), bottom({bottomHandle})");

            newSize = new Vector2(
                newSize.x - leftHandle + rightHandle,
                newSize.y + topHandle - bottomHandle
            );

            newPosition = new Vector2(
                newPosition.x + leftHandle * .5f + rightHandle * .5f,
                newPosition.y + topHandle * .5f + bottomHandle * .5f);

            Rect newRect = new Rect();
            newRect.size = newSize;
            newRect.center = newPosition;

            return newRect;
        }
    }
}
