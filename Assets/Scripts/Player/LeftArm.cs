using UnityEngine;
using UnityEngine.InputSystem;

public class LeftArm : MonoBehaviour
{
    public GameObject player;
    Rigidbody2D rb;
    [SerializeField] float impulseSpeed;
    Vector2 armAngle;

    void OnEnable()
    {
        InputSystem.actions.FindAction("ImpulseL").performed += Impulse;
    }

    void OnDisable()
    {
        InputSystem.actions.FindAction("ImpulseL").performed -= Impulse;
    }

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 leftInputVector = Gamepad.current.leftStick.ReadValue();
        RotateLeftArm(leftInputVector);
    }

    private void RotateLeftArm(Vector2 inputDirection)
    {
        armAngle = inputDirection;
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle + 180 * -1);
        transform.rotation = rotation;
    }

    private void Impulse(InputAction.CallbackContext context)
    {
        rb.linearVelocity = armAngle * impulseSpeed;
    }
}
