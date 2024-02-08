using System;
using UnityEngine;

namespace BaseTool.Shooter
{
    [AddComponentMenu("BaseTool/Shooter/Shoot Controller")]
    public class ShootController : MonoBehaviour, IShootable, IShootController
    {
        public virtual bool CanShoot => true;

        public event Action OnStartShoot;
        public event Action OnStopShoot;
        public event Action OnReload;

        public virtual void ShootPressed()
        {
            OnStartShoot?.Invoke();
        }

        public virtual void ShootReleased()
        {
            OnStopShoot?.Invoke();
        }

        public virtual void Reload()
        {
            OnReload?.Invoke();
        }
    }
}