using UnityEngine;

namespace BaseTool.Movement
{
    public interface IMovable
    {
        void Move(Vector2 move);

        void Rotate(Vector2 rotation);
    }

}