using System;
using BaseTool.Tools;
using BaseTool.Tools.Attributes;
using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/First Person Controller")]
    [RequireComponent(typeof(Rigidbody))]
    public class FirstPersonController : MonoBehaviour, IMovable
    {
        [GetComponent]
        private Rigidbody _rigidbody;

        [GetComponentInChildren]
        private Camera _camera;

        [Header("Movement Settings")]
        [SerializeField]
        private float _speed = 5;

        [SerializeField]
        private float _lookSpeed = 5;

        private Vector2 _moveInput = Vector2.zero;
        private Vector2 _rotationInput = Vector2.zero;

        private float _cameraRotation = 0;

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void FixedUpdate() => FixedUpdateMove();

        protected void FixedUpdateMove()
        {
            Vector3 tmp = new Vector3(_moveInput.x, 0, _moveInput.y);
            var dir = _speed * transform.TransformDirection(tmp);
            dir.y = _rigidbody.velocity.y;
            _rigidbody.velocity = dir;
        }

        public void Move(Vector2 move)
        {
            _moveInput = move;
        }

        public void Rotate(Vector2 rotation)
        {
            _rotationInput = rotation;
            var newRot = _rigidbody.rotation * Quaternion.Euler(_lookSpeed * Time.fixedDeltaTime * _rotationInput.x * Vector3.up);
            _rigidbody.MoveRotation(newRot);
            _cameraRotation = Mathf.Clamp(_cameraRotation + _lookSpeed * _rotationInput.y * Time.fixedDeltaTime, -90, 90);

            _camera.transform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);
        }
    }
}