using UnityEngine;

namespace BaseTool.Shooter
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "BaseTool/Shooter/Weapon")]
    public class Weapon : ScriptableObject
    {
        #region DATA

        /// <summary>
        /// Weapon displaying name (for UI purpose e.g.).
        /// </summary>
        [Header("Informations")]
        public string Title;

        /// <summary>
        /// Weapon description (for UI purpose e.g.).
        /// </summary>
        [TextArea(2, 5)]
        public string Description;

        /// <summary>
        /// Weapon icon (for UI purpose e.g.).
        /// </summary>
        public Sprite Icon;

        /// <summary>
        /// Category used to understand what kind of weapon it is.
        /// Mostly used to sorting weapons, collecting munitions or identifying weapons. 
        /// </summary>
        public WeaponCategory Category;

        /// <summary>
        /// Is the weapon automatic or not?
        /// If true, you can shot by keeping the shot button down.
        /// If false, you have to release the shot button to shot again.
        /// </summary>
        [Header("Stats")]
        public bool Automatic = true;

        /// <summary>
        /// Damages done by each bullet.
        /// </summary>
        public int Damages = 1;

        /// <summary>
        /// Number of maximum ammo in the weapon.
        /// </summary>
        public int Ammo = 30;

        /// <summary>
        /// Number of bullets used each shot.
        /// Used for burst fires.
        /// </summary>
        public int AmmoPerShot = 1;

        /// <summary>
        /// Time to reload weapon, it couldn't shoot during reloading.
        /// </summary>
        public Cooldown ReloadingTime = 1;

        /// <summary>
        /// Number of shot by seconds.
        /// </summary>
        public float FireRate = 1;

        /// <summary>
        /// Does the weapon shot a raycast (hitscan) or a projectile?
        /// </summary>
        public bool UseProjectile = false;

        /// <summary>
        /// Maximum distance of the raycast.
        /// </summary>
        [IfNot(nameof(UseProjectile))]
        public float Range = 200;

        /// <summary>
        /// LayerMask used for the raycast during shoot.
        /// </summary>
        [IfNot(nameof(UseProjectile))]
        public LayerMask HitMask;

        /// <summary>
        /// Maximum number of elements touched by each shot.
        /// By default: 1.
        /// </summary>
        [IfNot(nameof(UseProjectile))]
        public uint MaxTraversalCasts = 1;

        /// <summary>
        /// The GameObject to instantiate when using or equiping the weapon.
        /// </summary>
        [Header("References")]
        public GameObject Prefab;

        /// <summary>
        /// If <see cref="UseProjectile"/> is true, the GameObject to instantiate as a projectile.
        /// </summary>
        [If(nameof(UseProjectile))]
        public GameObject ProjectilePrefab;

        #endregion

        /// <summary>
        /// Current number of ammo in the weapon.
        /// </summary>
        [HideInInspector]
        public int CurrentAmmo;

        /// <summary>
        /// The time to wait before another shoot could be performed.
        /// </summary>
        private Cooldown _shootCooldown = 0;

        /// <summary>
        /// Can the weapon shoots?
        /// </summary>
        public bool CanShoot => !IsReloading && _shootCooldown.IsReady && CurrentAmmo > 0 && IsNonAutomaticReady;

        /// <summary>
        /// Can the weapon shoots?
        /// </summary>
        public bool IsNonAutomaticReady => Automatic || !HasShot;

        /// <summary>
        /// Does the weapon is currently reloading?
        /// </summary>
        public bool IsReloading => !ReloadingTime.IsReady;

        /// <summary>
        /// Symbolic link to get code more readable.
        /// </summary>
        public int MaxAmmo => Ammo;

        /// <summary>
        /// Does the shoot process has been started?
        /// This boolean will be set to false when the shoot button is released.
        /// </summary>
        public bool HasShot { get; protected set; } = false;

        public void Initialize()
        {
            CurrentAmmo = Ammo;
            _shootCooldown = 1f / FireRate;
            ReloadingTime.OnReady += OnReloadFinished;
        }

        private void OnReloadFinished()
        {
            CurrentAmmo = Ammo;
        }

        public void Reload()
        {
            if (IsReloading || Ammo == CurrentAmmo)
                return;
            ReloadingTime.Reset();
        }

        public void Update()
        {
            ReloadingTime.Update(Time.deltaTime);
            _shootCooldown.Update(Time.deltaTime);
        }

        public void Shoot()
        {
            _shootCooldown.Reset();
            CurrentAmmo -= AmmoPerShot;
            HasShot = true;
        }

        public void ReleaseShoot()
        {
            HasShot = false;
        }
    }
}
