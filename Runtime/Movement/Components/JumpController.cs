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
        protected int _additionalJumpsLeft = 0;

        public bool IsGrounded { get; protected set; } = true;

        public bool CanJump => _additionalJumpsLeft > 0 || CanCoyoteEffect || (!_isJumping && IsGrounded);

        public bool CanCoyoteEffect => !IsGrounded && !_coyoteEffectTiming.IsReady;

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

        protected virtual void Start()
        {
            _coyoteEffectTiming = _coyoteEffectDelay;
            _coyoteEffectTiming.SubscribeToManager = false;
            ResetAdditionalJumps();
        }

        protected virtual void FixedUpdate()
        {
            CheckGrounded();

            if (Velocity.y < 0)
            {
                var velocity = Velocity;
                velocity.y *= _fallMultiplier;
                Velocity = velocity;
            }
        }

        public virtual void Jump()
        {
            if (!CanJump) return;

            _additionalJumpsLeft--;
            _coyoteEffectTiming = 0;

            var velocity = Velocity;
            velocity.y = _jumpForce;
            Velocity = velocity;

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
                ResetAdditionalJumps();
            }
            IsGrounded = colliders.Length != 0;
        }

        protected void ResetAdditionalJumps() => _additionalJumpsLeft = _jumpCount - 1;

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + _groundCheckOffset, _groundCheckSize);
        }
    }
}