using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTool.FPS
{
    [AddComponentMenu("BaseTool/FPS/PlayerMovement")]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        protected Camera _playerCamera;

        public float MoveSpeed = 1;

        public float GravityMultiplier = 1;

        public float JumpHeight = 1;

        public float JumpForce = 1;

        protected Vector3 _movement;

        protected CharacterController _characterController;

        protected float _jumpValue = 0;

        protected Vector2 _inputMovement;
        protected bool _jump;
        protected bool _isGrounded;

        // Start is called before the first frame update
        void Start()
        {
            _characterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _jumpValue = Physics.gravity.y;
        }

        // Update is called once per frame
        void Update()
        {
            Look(new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")
            ));

            _inputMovement = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );
            _jump = Input.GetButton("Jump");

            Move(_inputMovement, _jump);

            //rot.x = Mathf.Clamp(rot.x, -90, 90);
        }

        private void FixedUpdate()
        {
            if (_characterController.isGrounded)
                _jumpValue = -0.1f;
            else
                _jumpValue += Time.fixedDeltaTime * Physics.gravity.y;

            _isGrounded = _characterController.isGrounded;
        }

        public void Move(Vector2 inputs, bool jump)
        {
            if(jump)
                Debug.Log("Jump !");

            if (jump && _isGrounded)
            {
                Debug.Log("Jump is grounded !");
                _jumpValue = JumpHeight;
            }

            _movement = new Vector3(
                inputs.x,
                _jumpValue,
                inputs.y
            );
            _characterController.Move(
                transform.TransformDirection(_movement) * MoveSpeed * Time.deltaTime
            );
        }

        public void Look(Vector2 input)
        {
            transform.Rotate(transform.up, input.x);

            var rot = _playerCamera.transform.localEulerAngles;
            rot.x += input.y;

            if(rot.x < 0 || rot.x > 360 || (0 <= rot.x && rot.x <= 90) || (270 <= rot.x && rot.x <= 360))
            {
                Debug.Log("In Range");
            }
            else
            {
                rot.x = NearestBetween(rot.x, 90, 270);
            }
            _playerCamera.transform.localEulerAngles = rot;
        }

        private float NearestBetween(float value, float limit1, float limit2)
        {
            float diff1 = Mathf.Abs(value - limit1),
                diff2 = Mathf.Abs(value - limit2);

            return diff1 < diff2 ? limit1 : limit2;
        }
    }
}