using BaseTool.Tools;
using BaseTool.Tools.Attributes;
using UnityEngine;

namespace BaseTool.Shooter
{
    [AddComponentMenu("BaseTool/Old Shoot Input")]
    public class OldShootInput : MonoBehaviour
    {
        [SerializeField]
        private string _shootButton = "Fire1";

        [GetComponent]
        private IShootable _shootableComponent;

        protected virtual void Awake() => Injector.Process(this);

        void Update()
        {
            if (!Input.GetButton(_shootButton) || _shootableComponent == null) return;
            _shootableComponent.Shoot();
        }
    }
}
