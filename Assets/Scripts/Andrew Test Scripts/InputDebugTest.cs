using UnityEngine;
using UnityEngine.InputSystem;
using CustomEvents.Andrew;
using System;
public class InputDebugTest : MonoBehaviour, IDelegateArray
{
    public event Action<float> TestEvent;
    enum CustomEvents
    {
        HitSpace
    }
    private readonly IDelegateArray.EnumDelegate[] _enumEvents = new IDelegateArray.EnumDelegate[1];
    public IDelegateArray.EnumDelegate[] EnumEvents => _enumEvents;

    void OnEnable()
    {
        InputSystem.actions.FindAction("Jump").performed += Print;
    }
    private void OnDisable()
    {
        InputSystem.actions.FindAction("Jump").performed -= Print;
    }
    void Print(InputAction.CallbackContext context)
    {
        TestEvent?.Invoke(2.0f);
        _enumEvents[(int)CustomEvents.HitSpace]?.Invoke();
    }
}
