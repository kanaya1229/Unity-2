
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerController>() == null)
        {
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddKey();
            Destroy(gameObject);
        }
    }
}