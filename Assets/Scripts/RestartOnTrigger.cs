using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent(out ArmManager _))
        {
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
