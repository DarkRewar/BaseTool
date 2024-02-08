using System;

namespace BaseTool.Shooter
{
    public interface IShootController
    {
        public event Action OnStartShoot;

        public event Action OnStopShoot;

        public event Action OnReload;
    }
}
