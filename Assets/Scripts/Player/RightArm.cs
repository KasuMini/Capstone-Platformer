using UnityEngine;
using UnityEngine.InputSystem;

public class RightArm : MonoBehaviour
{
    public GameObject player;
    Rigidbody2D rb;
    [SerializeField] float impulseSpeed;
    Vector2 armAngle;

    void OnEnable()
    {
        InputSystem.actions.FindAction("ImpulseR").performed += Impulse;
    }

    void OnDisable()
    {
        InputSystem.actions.FindAction("ImpulseR").performed -= Impulse;
    }

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 rightInputVector = Gamepad.current.rightStick.ReadValue();
        RotateRightArm(rightInputVector);
    }

    private void RotateRightArm(Vector2 inputDirection)
    {
        armAngle = inputDirection;
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }

    private void Impulse(InputAction.CallbackContext context)
    {
        rb.linearVelocity = armAngle * impulseSpeed;
    }


}
