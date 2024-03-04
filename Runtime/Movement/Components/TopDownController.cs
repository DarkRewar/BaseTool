using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/Top Down Controller")]
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

        [SerializeField]
        protected bool _twinStickMovement = false;

        [SerializeField, If(nameof(_twinStickMovement))]
        protected bool _useCursorToLook = true;

        [SerializeField, IfNot(nameof(_twinStickMovement))]
        protected bool _orientWithVelocity = true;

        protected Vector3 _movement;
        protected Vector3 _previousVelocity;
        protected Plane _playerPlane;

        protected virtual void Awake() => Injector.Process(this);

        private void Start()
        {
            _playerPlane = new Plane(Vector3.up, transform.position);
        }

        protected virtual void Update()
        {
            _playerPlane.SetNormalAndPosition(Vector3.up, transform.position);

            if (_twinStickMovement)
            {
                Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
                if (_playerPlane.Raycast(ray, out float enter))
                {
                    Vector3 point = ray.GetPoint(enter);
                    float angle = Vector3.SignedAngle(Vector3.forward, point - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
            }

            if (_twinStickMovement || _rigidbody.velocity.magnitude <= 0.01f) return;
            transform.forward = _rigidbody.velocity.ChangeY(0);
            _previousVelocity = _rigidbody.velocity;
        }

        protected virtual void FixedUpdate()
        {
            var newMovement = Quaternion.Euler(0, _playerCamera.transform.eulerAngles.y, 0) * _movement.LimitLength();
            _rigidbody.velocity = (_moveSpeed * newMovement).ChangeY(_rigidbody.velocity.y);
        }

        protected virtual void OnApplicationFocus(bool focus)
        {
            Cursor.visible = focus && _twinStickMovement;
        }

        public void Move(Vector2 move)
        {
            _movement.x = move.x;
            _movement.z = move.y;
        }

        public void Rotate(Vector2 rotation)
        {
            if (!_twinStickMovement) return;

            var angle = _playerCamera.transform.eulerAngles.y + Vector2.SignedAngle(Vector2.up, rotation);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
