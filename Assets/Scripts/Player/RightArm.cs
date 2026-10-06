using UnityEngine;
using UnityEngine.InputSystem;

public class RightArm : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Vector2 rightInputVector = Gamepad.current.rightStick.ReadValue();
        RotateRightArm(rightInputVector);
    }

    private void RotateRightArm(Vector2 inputDirection)
    {
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }

}
