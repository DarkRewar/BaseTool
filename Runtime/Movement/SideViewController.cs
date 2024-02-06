using BaseTool.Tools;
using BaseTool.Tools.Attributes;
using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Side View Controller")]
    [RequireComponent(typeof(Rigidbody))]
    public class SideViewController : MonoBehaviour, IMovable
    {
        [SerializeField] private float _moveSpeed = 5;

        [GetComponent] private Rigidbody _rigidbody;

        void Awake() => Injector.Process(this);

        public void Move(Vector2 move)
        {
            var velocity = _rigidbody.velocity;
            velocity.x = _moveSpeed * move.x;
            _rigidbody.velocity = velocity;
        }

        public void Rotate(Vector2 _) { }
    }
}