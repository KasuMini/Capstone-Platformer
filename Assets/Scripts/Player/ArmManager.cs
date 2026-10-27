using UnityEngine;
using UnityEngine.InputSystem;
using Items.Andrew;

public class ArmManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    private InputAction _leftArmInput;
    private InputAction _rightArmInput;

    [SerializeField] private Transform _leftArmPivot;
    [SerializeField] private Transform _rightArmPivot;

    [SerializeField] private ImpulseItem _rightArmItem;
    [SerializeField] private ImpulseItem _leftArmItem;

    [SerializeField] private float maxTargetAngleDecay = 25f;
    [SerializeField] private float minTargetAngleDecay = 1f;
    [SerializeField] private float magnitudeForSnapAngle = 0.9f;
    private float currentAngle;

    private float angleVelocity = 0f;


    private void Awake()
    {

        _leftArmInput = _playerInput.actions.FindAction("Left Arm");
        _rightArmInput = _playerInput.actions.FindAction("Right Arm");
        _rightArmItem.InitTriggerAction(_playerInput.actions.FindAction("Right Trigger"));
        _leftArmItem.InitTriggerAction(_playerInput.actions.FindAction("Left Trigger"));
    }
    /*
    private void OnEnable()
    {
        _playerInput.actions.FindAction("Left Trigger").performed += LeftArmTriggered;
        _playerInput.actions.FindAction("Right Trigger").performed += RightArmTriggered;


    }
    private void OnDisable()
    {
        _playerInput.actions.FindAction("Left Trigger").performed -= LeftArmTriggered;
        _playerInput.actions.FindAction("Right Trigger").performed -= RightArmTriggered;
    }
    */

    //useful decay range is 1-25 - slow to fast
    private float ExpDecay(float deltAngle, float targetAngle, float decay, float dt)
    {
        //b + (a - b) * Mathf.Exp(-decay * dt);
        return targetAngle + deltAngle * Mathf.Exp(-decay * dt);
    }
    private void Update()
    {
        //print(_leftArmInput.ReadValue<Vector2>());
        SetDirection(_leftArmInput.ReadValue<Vector2>(), _leftArmPivot);
        SetDirection(_rightArmInput.ReadValue<Vector2>(), _rightArmPivot);
    }

    
    private void SetDirection(Vector2 directionInput, Transform armPivotTransform)
    {
        float magnitude = directionInput.magnitude;
        if (magnitude > magnitudeForSnapAngle)
        {
            armPivotTransform.right = directionInput;
            return;
        }
        //lerps toward direcgtion based on input magnitude
        float targetAngle = Vector2.SignedAngle(Vector2.right, directionInput);
        currentAngle = Vector2.SignedAngle(Vector2.right, armPivotTransform.right);
        float deltaAngle = Mathf.DeltaAngle(currentAngle, targetAngle);


        float t = magnitude / magnitudeForSnapAngle;
        float decay = Mathf.Lerp(minTargetAngleDecay, maxTargetAngleDecay, t);
        float newAngle = ExpDecay(-deltaAngle, targetAngle, decay, Time.deltaTime);

        armPivotTransform.eulerAngles = Vector3.forward * newAngle;
    }
    public void LeftArmTriggered(InputAction.CallbackContext context)
    {
    }

    //might be better just to pass in the input action when possessing item, so the item itself can control how it handles input
    public void RightArmTriggered(InputAction.CallbackContext context)
    {
        //_rightArmItem.InputTriggered(context.ReadValue<float>());
    }
}
