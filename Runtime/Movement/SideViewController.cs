using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/Side View Controller")]
    [RequireComponent(typeof(Rigidbody))]
    public class SideViewController : MonoBehaviour, IMovable
    {
        [SerializeField] private float _moveSpeed = 5;

        [GetComponent] private Rigidbody _rigidbody;

        void Awake() => Injector.Process(this);

        public void Move(Vector2 move)
        {
#if UNITY_2023_3_OR_NEWER
            var velocity = _rigidbody.linearVelocity;
            velocity.x = _moveSpeed * move.x;
            _rigidbody.linearVelocity = velocity;
#else
            var velocity = _rigidbody.velocity;
            velocity.x = _moveSpeed * move.x;
            _rigidbody.velocity = velocity;
#endif
        }

        public void Rotate(Vector2 _) { }
    }
}