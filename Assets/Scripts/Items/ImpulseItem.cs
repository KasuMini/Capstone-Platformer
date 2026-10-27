using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using CustomEvents.Andrew;

namespace Items.Andrew
{
    public class ImpulseItem : PlayerItem, IDelegateArray
    {
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

        public override void TriggerAction()
        {
            TrackAmmoAndApplyForce();
        }
        private void TrackAmmoAndApplyForce()
        {
            _ammo--;
            if (_ammo < 0)
            {
                _enumEvents[(int)CustomEvents.ImpulseFail]?.Invoke();
                return;
            }
            if (_ammo == 0)
            {
                StartCoroutine(CoolDown());
            }
            _enumEvents[(int)CustomEvents.ImpulseSuccess]?.Invoke();

            RB2D.linearVelocity -= (Vector2)transform.right * _impulseForce;
        }
        IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(_coolDownTime);
            _ammo = _maxAmmo;
        }
    }
}

