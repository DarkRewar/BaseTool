using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/Old Movement Input")]
    public class OldMovementInput : MonoBehaviour
    {
        [SerializeField]
        private bool _invertXAxis = false;

        [SerializeField]
        private bool _invertYAxis = false;

        [SerializeField]
        private string _jumpButton = "Jump";

        [GetComponent]
        private IMovable _movableComponent;

        [GetComponent]
        private IJumpable _jumpableComponent;

        void Awake() => Injector.Process(this);

        void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            UpdateMovement();
            UpdateJump();
        }

        private void UpdateJump()
        {
            if (_jumpableComponent == null) return;

            if (Input.GetButtonDown(_jumpButton))
                _jumpableComponent.Jump();
        }

        private void UpdateMovement()
        {
            if (_movableComponent == null) return;

            Vector2 move = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));

            Vector2 rotate = new Vector2(
                (_invertXAxis ? -1 : 1) * Input.GetAxis("Mouse X"),
                (_invertYAxis ? -1 : 1) * Input.GetAxis("Mouse Y"));

            _movableComponent.Move(move);
            _movableComponent.Rotate(rotate);
        }
    }
}