using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    [SerializeField] private Transform wheelSprite;
    [SerializeField] private float _radius = .5f;
    private float _speed = 0;


    private void OnCollisionStay2D(Collision2D collision)
    {
        _speed = Mathf.Sign(collision.relativeVelocity.x) * collision.relativeVelocity.magnitude;
    }
    private void Update()
    {
        float angularVelocity = (_speed / _radius) * Mathf.Rad2Deg;
        Vector3 rotationDelta = angularVelocity * Time.deltaTime * Vector3.forward;
        wheelSprite.Rotate(rotationDelta);
    }
}
