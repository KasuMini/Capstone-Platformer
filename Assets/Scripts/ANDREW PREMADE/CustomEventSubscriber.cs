using UnityEngine;

namespace CustomEvents.Andrew
{
    public abstract class CustomEventSubscriber: MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _eventSender;
        [SerializeField] private int _eventIndex = 0;
        private IDelegateArray.EnumDelegate[] _enumEvents;

        private void Awake()
        {
            _enumEvents = ((IDelegateArray)_eventSender).EnumEvents;
        }
        private void OnEnable()
        {
            _enumEvents[_eventIndex] += EventCalled;
        }
        private void OnDisable()
        {
            _enumEvents[_eventIndex] -= EventCalled;

        }

        public abstract void EventCalled();
    }
}
