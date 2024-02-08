using UnityEngine;

namespace BaseTool.Shooter
{
    [AddComponentMenu("BaseTool/Shooter/Old Shoot Input")]
    public class OldShootInput : MonoBehaviour
    {
        [SerializeField]
        protected string _shootButton = "Fire1";

        [SerializeField]
        protected string _reloadButton = "Fire2";

        [GetComponent]
        protected IShootable _shootableComponent;

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void Update()
        {
            if (_shootableComponent == null) return;

            if (Input.GetButtonDown(_reloadButton))
                _shootableComponent.Reload();

            if (Input.GetButtonDown(_shootButton))
                _shootableComponent.ShootPressed();
            else if (Input.GetButtonUp(_shootButton))
                _shootableComponent.ShootReleased();
        }
    }
}
