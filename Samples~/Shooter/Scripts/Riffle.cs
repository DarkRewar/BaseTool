using BaseTool.Shooter;
using TMPro;
using UnityEngine;

namespace BaseTool.Samples.Shooter
{
    public class Riffle : MonoBehaviour
    {
        [SerializeField]
        protected TMP_Text _ammoText;

        [GetComponentInParent, SerializeField]
        protected WeaponController _weaponController;

        private void Awake() => Injector.Process(this);

        void LateUpdate()
        {
            _ammoText.text = $"{_weaponController.Ammo}";
        }
    }
}
