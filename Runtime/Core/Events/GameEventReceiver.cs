using UnityEngine;
using UnityEngine.Events;

namespace BaseTool
{
    [AddComponentMenu("BaseTool/Events/Game Event Receiver")]
    public class GameEventReceiver : MonoBehaviour
    {
        [SerializeField]
        protected GameEvent _gameEvent;

        public UnityEvent OnTriggered;

        protected virtual void OnEnable()
        {
            _gameEvent.OnTriggered += Trigger;
        }

        protected virtual void OnDisable()
        {
            _gameEvent.OnTriggered -= Trigger;
        }

        protected void Trigger(object _) => OnTriggered?.Invoke();
    }
}
