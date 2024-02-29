using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/Jump Controller")]
    [RequireComponent(typeof(Rigidbody))]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#jumpcontroller")]
    public class JumpController : MonoBehaviour, IJumpable
    {
        [GetComponent, SerializeField] protected Rigidbody _rigidbody;

        [Header("Jump Settings")]
        [SerializeField]
        protected float _jumpForce = 10;

        [SerializeField]
        protected float _fallMultiplier = 1;

        [SerializeField]
        protected int _jumpCount = 1;

        [Header("Ground Check Settings")]
        [SerializeField]
        protected LayerMask _groundMask;

        [SerializeField] private Vector3 _groundCheckOffset = default;
        [SerializeField] private float _groundCheckSize = 0.2f;
        [SerializeField] private float _coyoteEffectDelay = default;

        protected Cooldown _coyoteEffectTiming;
        protected bool _isJumping = false;
        protected int _jumpsLeft = 1;

        public bool IsGrounded { get; protected set; } = true;

        public bool CanJump => _jumpsLeft > 1 || CanCoyoteEffect || (!_isJumping && IsGrounded);

        public bool CanCoyoteEffect => !IsGrounded && !_coyoteEffectTiming.IsReady;

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void Start()
        {
            _coyoteEffectTiming = _coyoteEffectDelay;
            _jumpsLeft = _jumpCount;
        }

        protected virtual void FixedUpdate()
        {
            CheckGrounded();

#if UNITY_2023_3_OR_NEWER
            if (_rigidbody.linearVelocity.y < 0)
            {
                var velocity = _rigidbody.linearVelocity;
                velocity.y *= _fallMultiplier;
                _rigidbody.linearVelocity = velocity;
            }
#else

            if (_rigidbody.velocity.y < 0)
            {
                var velocity = _rigidbody.velocity;
                velocity.y *= _fallMultiplier;
                _rigidbody.velocity = velocity;
            }
#endif
        }

        public virtual void Jump()
        {
            if (!CanJump) return;

            _jumpsLeft--;
            _coyoteEffectTiming = 0;
#if UNITY_2023_3_OR_NEWER
            var velocity = _rigidbody.linearVelocity;
            velocity.y = _jumpForce;
            _rigidbody.linearVelocity = velocity;
#else
            var velocity = _rigidbody.velocity;
            velocity.y = _jumpForce;
            _rigidbody.velocity = velocity;
#endif
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
                _jumpsLeft = _jumpCount;
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