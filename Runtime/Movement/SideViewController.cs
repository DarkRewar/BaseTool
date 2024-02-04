using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Side View Controller")]
    public class SideViewController : MonoBehaviour, IMovable, IJumpable
    {
        public bool CanJump => throw new System.NotImplementedException();

        public void Jump()
        {
            throw new System.NotImplementedException();
        }

        public void Move(Vector2 move)
        {
            throw new System.NotImplementedException();
        }

        public void Rotate(Vector2 rotation)
        {
            throw new System.NotImplementedException();
        }
    }
}