using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using CustomEvents.Andrew;

namespace Items.Andrew
{
    public class ImpulseItem : PlayerItem, IDelegateArray
    {
        [SerializeField] private Rigidbody2D _rb2D;
        [SerializeField] private float _impulseForce;
        [SerializeField] private float _coolDownTime;
        [SerializeField] private int _maxAmmo = 2;
        private int _ammo = 0;

        enum CustomEvents
        {
            ImpulseSuccess,
            ImpulseFail
        }
        private readonly IDelegateArray.EnumDelegate[] _enumEvents = new IDelegateArray.EnumDelegate[2];
        public IDelegateArray.EnumDelegate[] EnumEvents => _enumEvents;

        void Start()
        {
            _ammo = _maxAmmo;
        }
        public override void SubscribeToAction()
        {
            _triggerAction.started += Impulse;
        }
        private void OnDisable()
        {
            _triggerAction.started -= Impulse;
        }

        private void Impulse(InputAction.CallbackContext context)
        {
            if(_ammo <= 0)
            {
                _enumEvents[(int)CustomEvents.ImpulseFail]?.Invoke();
                if(_ammo == 0)
                {
                    StartCoroutine(CoolDown());
                }
                return;
            }
            _enumEvents[(int)CustomEvents.ImpulseSuccess]?.Invoke();
            _ammo--;
            _rb2D.linearVelocity -= (Vector2)transform.right * _impulseForce;
            //_rb2D.AddForce(transform.right * _impulseForce, ForceMode2D.Impulse);
        }
        IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(_coolDownTime);
            _ammo = _maxAmmo;
        }
    }
}

