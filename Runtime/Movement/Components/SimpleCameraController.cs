using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/Simple Camera Controller")]
    public class SimpleCameraController : MonoBehaviour
    {
        [SerializeField]
        protected Transform _target;

        [SerializeField]
        protected Vector3 _offset = new Vector3(-5, 8, -5);

        [SerializeField]
        protected bool _followInstantly = false;

        [SerializeField, IfNot(nameof(_followInstantly))]
        protected float _followSpeed = 5;

        protected virtual void Update()
        {
            if (!_followInstantly) return;

            var destinationPosition = _target.position + _offset;
            transform.position = destinationPosition;
        }

        protected virtual void FixedUpdate()
        {
            if (_followInstantly) return;

            var destinationPosition = _target.position + _offset;
            transform.position = Vector3.Slerp(transform.position, destinationPosition, _followSpeed * Time.fixedDeltaTime);
        }

        [ContextMenu("Calculate Offset")]
        protected void CalculateOffset()
        {
            if (!_target) return;
            _offset = transform.position - _target.position;
        }
    }
}
