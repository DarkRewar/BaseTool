using System;
using UnityEngine;

namespace BaseTool
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "BaseTool/Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        public event Action<object> OnTriggered;

        public void Trigger() { OnTriggered?.Invoke(null); }

        public void Trigger(object obj) { OnTriggered?.Invoke(obj); }
    }
}
