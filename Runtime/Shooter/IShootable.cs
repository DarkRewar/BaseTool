namespace BaseTool.Shooter
{
    public interface IShootable
    {
        public bool CanShoot { get; }

        public void ShootPressed();

        public void ShootReleased();

        public void Shoot();
    }
}