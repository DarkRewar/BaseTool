using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/TopDown Controller")]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#topdowncontroller")]
    [RequireComponent(typeof(Rigidbody))]
    public class TopDownController : MonoBehaviour, IMovable
    {
        [SerializeField]
        protected Camera _playerCamera;

        [GetComponent, SerializeField]
        protected Rigidbody _rigidbody;

        [SerializeField]
        protected float _moveSpeed = 5;

        protected Vector3 _movement;
        protected Vector3 _previousVelocity;

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void Update()
        {
            if (_rigidbody.velocity.magnitude <= 0.01f) return;
            transform.forward = _rigidbody.velocity.ChangeY(0);
            _previousVelocity = _rigidbody.velocity;
        }

        protected virtual void FixedUpdate()
        {
            var newMovement = Quaternion.Euler(0, _playerCamera.transform.eulerAngles.y, 0) * _movement.LimitLength();
            _rigidbody.velocity = (_moveSpeed * newMovement).ChangeY(_rigidbody.velocity.y);
        }

        public void Move(Vector2 move)
        {
            _movement.x = move.x;
            _movement.z = move.y;
        }

        public void Rotate(Vector2 rotation) { }
    }
}
