using UnityEngine;
using CustomEvents.Andrew;

public class TestEventSubscriber: CustomEventSubscriber
{
    [SerializeField] string _printString = "event called";
    public override void EventCalled()
    {
        print(_printString);
    }
}
