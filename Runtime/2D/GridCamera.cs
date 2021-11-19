using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BaseTool.Generic.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BaseTool._2D
{
    //voir ça : https://forum.unity.com/threads/button-in-scene-view.259913/
    [RequireComponent(typeof(Camera)), AddComponentMenu("BaseTool/2D/GridCamera")]
    public class GridCamera : MonoBehaviour
    {
        public bool ShowGizmos = true;

        [SerializeField]
        public List<Bounds> Bounds = new List<Bounds>();

        public Transform Target;

        private int _currentBoundIndex = -1;
        private Bounds _currentBound;

        private Camera _camera;

        public void Start()
        {
            if(TryGetComponent(out Camera cam))
            {
                _camera = cam;
            }

            if(_currentBoundIndex == -1)
            {
                BindBounds();
            }
        }

        private void Update()
        {
            if (_currentBound.Contains(Target.position))
            {
                UpdateCamera();
            }
            else
            {
                BindBounds();
            }
        }

        private void UpdateCamera()
        {
            Vector3 newPos = Target.position;
            newPos = Vector3.Lerp(_camera.transform.position, newPos, Time.deltaTime * 2);

            float screenAspect = (float)Screen.width / (float)Screen.height;
            Debug.Log($"Screen {screenAspect}({Screen.width}x{Screen.height})");
            float cameraHeight = _camera.orthographicSize,
                cameraWidth = cameraHeight * screenAspect;
            Vector3 min = _currentBound.min,
                max = _currentBound.max;
            min.x += cameraWidth;
            min.y += cameraHeight;
            max.x -= cameraWidth;
            max.y -= cameraHeight;

            newPos = newPos.Clamp(
                min,
                max
            );
            newPos.z = _camera.transform.position.z;
            _camera.transform.position = newPos;
        }

        private void BindBounds()
        {
            _currentBoundIndex = FindBoundsForPoint(Target.position);
            _currentBound = Bounds[_currentBoundIndex];
        }

        private int FindBoundsForPoint(Vector3 point)
        {
            int i = 0;
            foreach (Bounds b in Bounds)
            {
                if (point.x.IsBetween(b.min.x, b.max.x) && point.y.IsBetween(b.min.y, b.max.y))
                    return i;
                ++i;
            }
            return -1;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                float screenAspect = (float)Screen.width / (float)Screen.height;
                float cameraHeight = _camera.orthographicSize,
                    cameraWidth = cameraHeight * screenAspect;
                Vector3 min = _currentBound.min,
                    max = _currentBound.max;
                min.x += cameraWidth;
                min.y += cameraHeight;
                max.x -= cameraWidth;
                max.y -= cameraHeight;

                Bounds b = new Bounds();
                b.min = min;
                b.max = max;

                Gizmos.color = Color.magenta;
                Gizmos.DrawWireCube(b.center, b.size);
            }
        }
    }
}
