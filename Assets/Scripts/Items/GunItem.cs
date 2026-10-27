using UnityEngine;
using System.Collections;

public class GunItem : PlayerItem
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private LayerMask _layerMask;
    public void Awake()
    {
        _line.enabled = false;
    }
    public override void TriggerAction()
    {
        RaycastHit2D hit  = Physics2D.Raycast(transform.position, transform.right, 20f, _layerMask);
        if (hit)
        {
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, hit.point);
            StartCoroutine(nameof(FlashLine));
        }
    }
    IEnumerator FlashLine()
    {
        _line.enabled = true;
        yield return new WaitForSeconds(.5f);
        _line.enabled = false;
    }
}
