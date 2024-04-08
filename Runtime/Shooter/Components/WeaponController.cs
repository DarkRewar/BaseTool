using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaseTool.Shooter
{
    [AddComponentMenu("BaseTool/Shooter/Weapon Controller")]
    public class WeaponController : MonoBehaviour
    {
        public bool DrawDebug = false;
        public bool AutoWeaponInstantiation = true;
        public bool UseCustomDamageHandler = false;

        public bool UseCameraToRaycast = true;

        [IfNot(nameof(UseCameraToRaycast))]
        public Transform ShootPoint;

        public Weapon Weapon;

        [GetComponentInParent]
        protected IShootController _iShootController;

        [GetComponentInParent, SerializeField]
        protected Camera _camera;

        public int Ammo => Weapon.CurrentAmmo;

        public int MaxAmmo => Weapon.Ammo;

        protected bool _askToShoot = false;
        protected GameObject _currentWeaponGameObject;

        protected Dictionary<Weapon, (Weapon instance, GameObject go)> _weaponInstances = new Dictionary<Weapon, (Weapon instance, GameObject go)>();

        public event OnShotTouchedEventHandler OnShotTouched;
        public delegate void OnShotTouchedEventHandler(Weapon weapon, ContactData[] contacts);

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void Start()
        {
            Equip(Weapon);
        }

        protected virtual void OnEnable()
        {
            _iShootController.OnStartShoot += PressShoot;
            _iShootController.OnStopShoot += ReleaseShoot;
            _iShootController.OnReload += OnReload;
        }

        protected virtual void OnDisable()
        {
            _iShootController.OnStartShoot -= PressShoot;
            _iShootController.OnStopShoot -= ReleaseShoot;
            _iShootController.OnReload -= OnReload;
        }

        protected virtual void Update()
        {
            if (Weapon) Weapon.Update();

            if (_askToShoot) OnShoot();
        }

        protected virtual void OnShoot()
        {
            if (!Weapon.CanShoot) return;
            Weapon.Shoot();

            ProcessRaycast();
            ProcessProjectile();
        }

        protected void ProcessRaycast()
        {
            if (Weapon.UseProjectile) return;

            var shootPoint = UseCameraToRaycast
                ? _camera.transform
                : ShootPoint;

            Ray ray = new Ray(shootPoint.position, shootPoint.forward);

            RaycastHit[] hits = Physics.RaycastAll(ray, Weapon.Range, Weapon.HitMask);
            if (hits.Length == 0)
                return;

            var pairs = hits
                .OrderBy(hit => hit.distance)
                .Select(hit => new ContactData
                {
                    Object = hit.collider.gameObject,
                    HitPoint = hit.point,
                    Normal = hit.normal
                })
                .Take((int)Weapon.MaxTraversalCasts)
                .ToArray();

            OnShotTouched?.Invoke(Weapon, pairs);

            if (!UseCustomDamageHandler) HandleDamages(pairs);

            DisplayDebugImpacts(pairs);
        }

        protected void ProcessProjectile()
        {
            if (!Weapon.UseProjectile) return;

            var position = _currentWeaponGameObject.transform.position + _currentWeaponGameObject.transform.forward;
            var rotation = _currentWeaponGameObject.transform.rotation;
            var projectile = Instantiate(Weapon.ProjectilePrefab, position, rotation);

            if (projectile.TryGetComponent(out WeaponProjectile weaponProjectile))
            {
                weaponProjectile.Damages = Weapon.Damages;
            }
        }

        protected virtual void PressShoot()
        {
            _askToShoot = true;
        }

        protected virtual void ReleaseShoot()
        {
            _askToShoot = false;
            Weapon.ReleaseShoot();
        }

        protected virtual void HandleDamages(ContactData[] pairs)
        {
            foreach (var pair in pairs)
            {
                if (pair.Object.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamages(Weapon.Damages);
            }
        }

        private void DisplayDebugImpacts(ContactData[] contacts)
        {
            if (!DrawDebug) return;

            foreach (ContactData contact in contacts)
            {
                var debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                debugSphere.transform.position = contact.HitPoint;
                debugSphere.transform.localScale = 0.35f * Vector3.one;
                debugSphere.GetComponent<Renderer>().material.color = Color.red;
                debugSphere.GetComponent<Collider>().enabled = false;
                Destroy(debugSphere, 0.5f);
            }
        }

        internal virtual void OnReload()
        {
            Weapon.Reload();
        }

        internal virtual void Equip(Weapon weapon)
        {
            Weapon weaponInstance;
            GameObject weaponObject;

            if (_weaponInstances.TryGetValue(weapon, out var pair))
            {
                weaponInstance = pair.instance;
                weaponObject = pair.go;
                weaponObject.SetActive(true);
            }
            else
            {
                weaponInstance = Instantiate(weapon);
                weaponInstance.Initialize();
                weaponObject = Instantiate(weapon.Prefab, transform);
                _weaponInstances.Add(weapon, (weaponInstance, weaponObject));
            }

            if (_currentWeaponGameObject)
            {
                _currentWeaponGameObject.SetActive(false);
            }

            Weapon = weaponInstance;
            _currentWeaponGameObject = weaponObject;
        }
    }
}
