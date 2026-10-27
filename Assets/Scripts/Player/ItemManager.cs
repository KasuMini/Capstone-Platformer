using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ArmManager _armManager;
    [SerializeField] private PlayerInput _playerInput;

    public PlayerItem ItemInCollider { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent(out PlayerItem item))
        {
            return;
        }
        ItemInCollider = item;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerItem item))
        {
            return;
        }
        if(ItemInCollider == item)
        {
            ItemInCollider = null;
        }
    }
}
