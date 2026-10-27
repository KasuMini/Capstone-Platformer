using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerItem : MonoBehaviour
{
    [System.NonSerialized] public Rigidbody2D RB2D;
    public abstract void TriggerAction();

    public void Equip(Transform armPivot, Rigidbody2D rb2D)
    {
        RB2D = rb2D;
        transform.parent = armPivot;
        transform.SetLocalPositionAndRotation(Vector3.right * 1.5f, Quaternion.identity);
    }
    public void UnEquip()
    {
        transform.parent = null;
    }
}
