using BaseTool.Shooter;
using TMPro;
using UnityEngine;

namespace BaseTool.Samples.Shooter
{
    public class AmmoDisplayer : MonoBehaviour
    {
        [SerializeField]
        protected TMP_Text _tmpText;

        [SerializeField]
        protected WeaponController _weaponController;

        private void LateUpdate()
        {
            _tmpText.text = $"{_weaponController.Ammo} / {_weaponController.MaxAmmo}";
        }
    }

}