using UnityEngine;
using UnityEngine.InputSystem;
using Items.Andrew;

public class ArmManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody2D _rb2D;
    private InputAction _leftArmInput;
    private InputAction _rightArmInput;

    [SerializeField] private Transform _leftArmPivot;
    [SerializeField] private Transform _rightArmPivot;

    private PlayerItem _rightArmItem;
    private PlayerItem _leftArmItem;

    [SerializeField] private PlayerItem _initRightArmItem;
    [SerializeField] private PlayerItem _initLeftArmItem;

    [SerializeField] private ItemManager _leftItemManager;
    [SerializeField] private ItemManager _rightItemManager;


    [SerializeField] private float maxTargetAngleDecay = 25f;
    [SerializeField] private float minTargetAngleDecay = 1f;
    [SerializeField] private float magnitudeForSnapAngle = 0.9f;
    private float currentAngle;

    private void Awake()
    {
        _leftArmInput = _playerInput.actions.FindAction("Left Arm");
        _rightArmInput = _playerInput.actions.FindAction("Right Arm");
    }
    private void Start()
    {
        EquipLeft(_initLeftArmItem);
        EquipRight(_initRightArmItem);
    }

    public void EquipRight(PlayerItem item)
    {
        if (_rightArmItem)
        {
            _rightArmItem.UnEquip();
        }
        item.Equip(_rightArmPivot, _rb2D);
        _rightArmItem = item;
    }
    public void EquipLeft(PlayerItem item)
    {
        if (_leftArmItem)
        {
            _leftArmItem.UnEquip();
        }
        item.Equip(_leftArmPivot, _rb2D);
        _leftArmItem = item;
    }

    private void OnEnable()
    {
        _playerInput.actions.FindAction("Left Trigger").started += LeftArmTriggered;
        _playerInput.actions.FindAction("Right Trigger").started += RightArmTriggered;

        _playerInput.actions.FindAction("Left Bumper").started += LeftBumperTriggered;
        _playerInput.actions.FindAction("Right Bumper").started += RightBumperTriggered;


    }
    private void OnDisable()
    {
        _playerInput.actions.FindAction("Left Trigger").started -= LeftArmTriggered;
        _playerInput.actions.FindAction("Right Trigger").started -= RightArmTriggered;

        _playerInput.actions.FindAction("Left Bumper").started -= LeftBumperTriggered;
        _playerInput.actions.FindAction("Right Bumper").started -= RightBumperTriggered;
    }
    

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
        _leftArmItem.TriggerAction();
    }

    public void LeftBumperTriggered(InputAction.CallbackContext context)
    {
        if (!_leftItemManager.ItemInCollider)
        {
            return;
        }
        EquipLeft(_leftItemManager.ItemInCollider);
    }

    //might be better just to pass in the input action when possessing item, so the item itself can control how it handles input
    public void RightArmTriggered(InputAction.CallbackContext context)
    {
        _rightArmItem.TriggerAction();
    }
    public void RightBumperTriggered(InputAction.CallbackContext context)
    {
        print("RIGHT");
        if (!_rightItemManager.ItemInCollider)
        {
            return;
        }
        EquipRight(_rightItemManager.ItemInCollider);
    }
}
