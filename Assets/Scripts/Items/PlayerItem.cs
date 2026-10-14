using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerItem : MonoBehaviour
{
    public InputAction _triggerAction;
    public void InitTriggerAction(InputAction action)
    {
        _triggerAction = action;
        SubscribeToAction();
    }
    public abstract void SubscribeToAction();
}
