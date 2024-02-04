using UnityEngine;

namespace BaseTool.Shooter
{
    [AddComponentMenu("BaseTool/Shoot Controller")]
    public class ShootController : MonoBehaviour, IShootable
    {
        public bool CanShoot => true;

        [SerializeField]
        private Cooldown _shootCooldown = 1;

        private bool _shootPressed = false;

        void Update()
        {
            _shootCooldown.Update();
            if (_shootPressed) Shoot();
        }

        public void Shoot()
        {
            if (_shootCooldown.IsReady)
            {
                _shootCooldown.Reset();
                Debug.Log("SHOOT!");
            }
        }

        public void ShootPressed()
        {
            _shootPressed = true;
        }

        public void ShootReleased()
        {
            _shootPressed = false;
        }
    }
}