using UnityEngine;

namespace BaseTool.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("BaseTool/Shooter/Weapon Projectile")]
    public class WeaponProjectile : MonoBehaviour
    {
        [SerializeField]
        protected Vector3 _initialVelocity = Vector3.forward;

        [GetComponent, SerializeField]
        protected Rigidbody _rigidbody;

        public int Damages;

        private void Awake() => Injector.Process(this);

        private void Start()
        {
            var newVelocity = transform.TransformDirection(_initialVelocity);
#if UNITY_2023_3_OR_NEWER
            _rigidbody.linearVelocity = newVelocity;
#else
            _rigidbody.velocity = newVelocity;
#endif
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamages(Damages);
            Destroy(gameObject);
        }
    }
}
