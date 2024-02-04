using BaseTool.Tools;
using BaseTool.Tools.Attributes;
using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Jump Controller")]
    [RequireComponent(typeof(Rigidbody))]
    public class JumpController : MonoBehaviour, IJumpable
    {
        [GetComponent] private Rigidbody _rigidbody;

        [Header("Jump Settings")]
        [SerializeField]
        private float _jumpForce = 10;

        [Header("Ground Check Settings")]
        [SerializeField]
        private LayerMask _groundMask;

        [SerializeField] private Vector3 _groundCheckOffset = default;
        [SerializeField] private float _groundCheckSize = 0.2f;
        [SerializeField] private float _coyoteEffectDelay = default;

        private Cooldown _coyoteEffectTiming;
        private bool _isJumping = false;

        public bool IsGrounded { get; protected set; } = true;

        public bool CanJump => !_isJumping && (IsGrounded || !_coyoteEffectTiming.IsReady);

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void Start() => _coyoteEffectTiming = _coyoteEffectDelay;

        protected virtual void FixedUpdate() => CheckGrounded();

        public virtual void Jump()
        {
            if (!CanJump) return;

            _coyoteEffectTiming = 0;
            _rigidbody.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
            _isJumping = true;
        }

        private void CheckGrounded()
        {
            _coyoteEffectTiming.Update(Time.fixedDeltaTime);

            Collider[] colliders = Physics.OverlapSphere(
                transform.position + _groundCheckOffset,
                _groundCheckSize,
                _groundMask);

            if (IsGrounded && colliders.Length == 0 && _coyoteEffectDelay > 0)
            {
                _coyoteEffectTiming = _coyoteEffectDelay;
                _coyoteEffectTiming.Reset();
            }
            else if (!IsGrounded && colliders.Length != 0)
            {
                _isJumping = false;
            }
            IsGrounded = colliders.Length != 0;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + _groundCheckOffset, _groundCheckSize);
        }
    }
}