using UnityEngine;

public class Wind: MonoBehaviour
{
    public Vector2 windDirection = Vector2.right;
    public float windStrength = 10f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.AddForce(windDirection.normalized * windStrength);
        }
    }
}
