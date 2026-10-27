using UnityEngine;

public class MaintainMomentum : MonoBehaviour
{
    private Rigidbody2D _rb2D;

    private float oldVelocity = 0f;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    private void LateUpdate()
    {
        oldVelocity = _rb2D.linearVelocity.magnitude;
    }

    private void Update()
    {
        //print(_rb2D.linearVelocity.magnitude);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 colVector = _rb2D.linearVelocity;
        float colVelocity = _rb2D.linearVelocity.magnitude;

        if (oldVelocity - colVelocity < 10f)
        {
            _rb2D.linearVelocity = colVector.normalized * oldVelocity;
        }
        _rb2D.gravityScale = 1.2f;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _rb2D.gravityScale = 1f;
    }
    //TRY INHERITENCE BASED STATE MACHINE

}
