using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/First Person Controller")]
    [RequireComponent(typeof(Rigidbody))]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#firstpersoncontroller")]
    public class FirstPersonController : MonoBehaviour, IMovable
    {
        [GetComponent, SerializeField]
        protected Rigidbody _rigidbody;

        [GetComponentInChildren, SerializeField]
        protected Camera _camera;

        [Header("Movement Settings")]
        [SerializeField]
        protected float _speed = 5;

        [SerializeField]
        protected float _lookSpeed = 5;

        protected Vector2 _moveInput = Vector2.zero;
        protected Vector2 _rotationInput = Vector2.zero;

        protected float _cameraRotation = 0;

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void FixedUpdate() => FixedUpdateMove();

        protected virtual void FixedUpdateMove()
        {
            Vector3 tmp = new Vector3(_moveInput.x, 0, _moveInput.y);
            var dir = _speed * transform.TransformDirection(tmp.LimitLength());

#if UNITY_2023_3_OR_NEWER
            dir.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = dir;
#else
            dir.y = _rigidbody.velocity.y;
            _rigidbody.velocity = dir;
#endif
        }

        public virtual void Move(Vector2 move)
        {
            _moveInput = move;
        }

        public virtual void Rotate(Vector2 rotation)
        {
            _rotationInput = rotation;
            var newRot = _rigidbody.rotation * Quaternion.Euler(_lookSpeed * Time.fixedDeltaTime * _rotationInput.x * Vector3.up);
            _rigidbody.MoveRotation(newRot);
            _cameraRotation = Mathf.Clamp(_cameraRotation + _lookSpeed * _rotationInput.y * Time.fixedDeltaTime, -90, 90);

            _camera.transform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);
        }
    }
}