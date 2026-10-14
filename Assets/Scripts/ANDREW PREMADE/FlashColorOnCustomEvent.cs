using UnityEngine;
using CustomEvents.Andrew;
using System.Collections;
public class FlashColorOnCustomEvent: CustomEventSubscriber
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashTime;
    private Color _initColor;
    public override void EventCalled()
    {
        _initColor = _sprite.color;
        StartCoroutine(FlashColor());
    }
    IEnumerator FlashColor()
    {
        _sprite.color = _flashColor;
        yield return new WaitForSeconds(_flashTime);
        _sprite.color = _initColor;
    }
}
