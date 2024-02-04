using BaseTool.Tools;
using BaseTool.Tools.Attributes;
using UnityEngine;

namespace BaseTool.FirstPerson
{
    [AddComponentMenu("BaseTool/FirstPerson")]
    [RequireComponent(typeof(Rigidbody))]
    public class FirstPersonController : MonoBehaviour
    {
        [GetComponent, SerializeField]
        private Rigidbody _rigidbody;

        [GetComponent]
        public Rigidbody Rigidbody { get; private set; }

        [GetComponentInChildren]
        public Collider ChildCollider;

        [GetComponentsInChildren, SerializeField]
        public Collider[] ChildrenColliders;

        [GetComponentInParent, SerializeField]
        public Transform Parent;

        void Awake() => Injector.Process(this);
    }
}