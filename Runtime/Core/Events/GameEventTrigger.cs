using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace BaseTool
{
    public enum GameEventTriggerType
    {
        Trigger,
        Collision,
        Both,
    }

    [AddComponentMenu("BaseTool/Events/Game Event Trigger")]
    [HelpURL("https://github.com/DarkRewar/BaseTool#game-events")]
    public class GameEventTrigger : MonoBehaviour
    {
        [Header("GameEventTrigger settings")]
        [SerializeField]
        protected bool _triggerOnce = true;

        [SerializeField]
        protected GameEventTriggerType _triggerType = GameEventTriggerType.Trigger;

        [SerializeField]
        protected List<string> _triggerTags = new List<string>() { "Player" };

        [SerializeField]
        protected GameEvent _gameEvent;

        [SerializeField]
        protected UnityEvent _genericEvents;

        protected bool _triggered = false;

        public virtual void OnTriggerEnter(Collider other)
        {
            if (_triggerType == GameEventTriggerType.Collision) return;
            if (!IsValidCompareTag(other) || (_triggerOnce && _triggered)) return;
            TriggerEvent();
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            if (_triggerType == GameEventTriggerType.Trigger) return;
            if (!IsValidCompareTag(collision.collider) || (_triggerOnce && _triggered)) return;
            TriggerEvent();
        }

        public void TriggerEvent()
        {
            if ((_triggerOnce && _triggered)) return;
            _triggered = true;
            _genericEvents?.Invoke();
            if (_gameEvent) _gameEvent.Trigger();
        }

        protected bool IsValidCompareTag(Collider collider) => _triggerTags.Any(tag => collider.CompareTag(tag));
    }
}
