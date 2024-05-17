using UnityEngine;

namespace BaseTool.Movement
{
    public enum ThirstPersonOrientation
    {
        OrientWithCamera,
        OrientWithMovement,
        Mixed
    }

    [AddComponentMenu("BaseTool/Movement/Third Person Controller")]
    [RequireComponent(typeof(Rigidbody))]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#thirdpersoncontroller")]
    public class ThirdPersonController : MonoBehaviour, IMovable
    {
        [GetComponent, SerializeField]
        protected Rigidbody _rigidbody;

        [Header("Camera Settings")]
        [SerializeField]
        protected Camera _camera;

        [SerializeField]
        protected float _playerHeight = 2; // 2 meters

        [SerializeField]
        protected float _cameraInitialDistance = 1.5f;

        [SerializeField, MinMax(-90, 90)]
        protected Vector2 _cameraMinMaxAngle = new Vector2(-60, 30);

        [Header("Controller Settings")]
        [SerializeField]
        protected ThirstPersonOrientation _orientation = ThirstPersonOrientation.Mixed;

        [Header("Movement Settings")]
        [SerializeField]
        protected float _speed = 5;

        [SerializeField]
        protected float _lookSpeed = 5;

        protected Vector2 _moveInput = Vector2.zero;
        protected Vector2 _rotationInput = Vector2.zero;

        protected Vector2 _cameraRotation = Vector2.zero;

        protected Vector3 Velocity
        {
#if UNITY_2023_3_OR_NEWER
            get => _rigidbody.linearVelocity;
            set => _rigidbody.linearVelocity = value;
#else
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
#endif
        }

        protected virtual void Awake() => Injector.Process(this);

        private void Update()
        {
            var targetLocalPosition = new Vector3(
                1f,
                _playerHeight - 0.3f,
                -_cameraInitialDistance - 1f);
            targetLocalPosition = Quaternion.Euler(0, _cameraRotation.x, 0) * targetLocalPosition;
            var worldTargetPosition = transform.position + targetLocalPosition;

            _camera.transform.SetPositionAndRotation(
                worldTargetPosition,
                Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0));

            OrientWithCamera();
            OrientWithMovement();
        }

        protected virtual void FixedUpdate() => FixedUpdateMove();

        protected virtual void FixedUpdateMove()
        {
            Vector3 tmp = new Vector3(_moveInput.x, 0, _moveInput.y);
            tmp = Quaternion.Euler(0, _cameraRotation.x, 0) * tmp;
            var dir = _speed * tmp.LimitLength();

            dir.y = Velocity.y;
            Velocity = dir;
        }

        public virtual void Move(Vector2 move)
        {
            _moveInput = move;
        }

        public virtual void Rotate(Vector2 rotation)
        {
            _cameraRotation += rotation;
            _cameraRotation.x %= 360;
            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, -_cameraMinMaxAngle.y, -_cameraMinMaxAngle.x);
        }

        protected virtual void OrientWithCamera()
        {
            if (_orientation == ThirstPersonOrientation.OrientWithMovement) return;
            if (_orientation == ThirstPersonOrientation.Mixed && Velocity.sqrMagnitude < 0.001f) return;

            transform.rotation = Quaternion.Euler(0, _cameraRotation.x, 0);
        }

        protected virtual void OrientWithMovement()
        {
            if (_orientation != ThirstPersonOrientation.OrientWithMovement) return;
            if (Velocity.sqrMagnitude < 0.001f) return;

            transform.rotation = Quaternion.LookRotation(Velocity.ChangeY(0), Vector3.up);
        }
    }
}