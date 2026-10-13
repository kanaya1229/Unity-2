
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Œ®‚ÉÚG: " + other.name);

        if (isCollected)
            return;

        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            Debug.Log("PlayerController‚ªŒ©‚Â‚©‚ç‚È‚¢");
            return;
        }

        Debug.Log("—EÒ‚ªŒ®‚ÉG‚ê‚½I");

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance‚ªnullI");
            return;
        }

        isCollected = true;

        GameManager.Instance.AddKey();

        Debug.Log("Œ®æ“¾¬Œ÷I");

        Destroy(gameObject);
    }
}
