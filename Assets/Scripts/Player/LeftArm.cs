using UnityEngine;
using UnityEngine.InputSystem;

public class LeftArm : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 leftInputVector = Gamepad.current.leftStick.ReadValue();
        RotateLeftArm(leftInputVector);
    }

    private void RotateLeftArm(Vector2 inputDirection)
    {
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle + 180 * -1);
        transform.rotation = rotation;
    }

}
