using UnityEngine;

namespace Items.Andrew
{
    public class ImpulseItem : PlayerItem
    {
        [SerializeField] private Rigidbody2D _rb2D;
        [SerializeField] private float _impulseForce;
        public override void InputTriggered(float axis)
        {
            _rb2D.linearVelocity = -transform.right * _impulseForce;
            //_rb2D.AddForce(transform.right * _impulseForce, ForceMode2D.Impulse);
        }
    }
}

