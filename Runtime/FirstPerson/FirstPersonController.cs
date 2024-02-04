using System;
using BaseTool.Tools;
using BaseTool.Tools.Attributes;
using UnityEngine;

namespace BaseTool.FirstPerson
{
    [AddComponentMenu("BaseTool/FirstPerson")]
    [RequireComponent(typeof(Rigidbody))]
    public class FirstPersonController : MonoBehaviour
    {
        [GetComponent]
        private Rigidbody _rigidbody;

        void Awake() => Injector.Process(this);
    }
}