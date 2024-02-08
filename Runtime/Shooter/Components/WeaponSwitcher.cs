using System.Collections.Generic;
using UnityEngine;

namespace BaseTool.Shooter
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField]
        protected WeaponController _weaponController;

        [SerializeField]
        protected List<Weapon> _weapons;

        protected int _currentWeaponIndex = 0;

        void Update()
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Approximately(scrollWheel, 0)) return;
            Switch((int)Mathf.Sign(scrollWheel));
        }

        public void Switch(int increment)
        {
            _currentWeaponIndex = MathUtils.Modulo(_currentWeaponIndex + increment, _weapons.Count);
            _weaponController.Equip(_weapons[_currentWeaponIndex]);
        }
    }
}
