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


    private void Awake()
    {

        _leftArmInput = _playerInput.actions.FindAction("Left Arm");
        _rightArmInput = _playerInput.actions.FindAction("Right Arm");
    }
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
    private void Update()
    {
        //print(_leftArmInput.ReadValue<Vector2>());
        SetDirection(_leftArmInput.ReadValue<Vector2>(), _leftArmPivot);
        SetDirection(_rightArmInput.ReadValue<Vector2>(), _rightArmPivot);
    }

    
    private void SetDirection(Vector2 directionInput, Transform armPivotTransform)
    {
        //should eventually include lerp towards the direction based on input magnitude
        armPivotTransform.right = directionInput;
    }
    public void LeftArmTriggered(InputAction.CallbackContext context)
    {
    }

    //might be better just to pass in the input action when possessing item, so the item itself can control how it handles input
    public void RightArmTriggered(InputAction.CallbackContext context)
    {
        _rightArmItem.InputTriggered(context.ReadValue<float>());
    }
}
