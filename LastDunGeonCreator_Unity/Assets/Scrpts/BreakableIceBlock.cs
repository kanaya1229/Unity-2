
using UnityEngine;

public class BreakableIceBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryBreak(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryBreak(other);
    }

    private void TryBreak(Collider2D other)
    {
        PlayerState state =
            other.GetComponentInParent<PlayerState>();

        if (state != null && state.IsBurning())
        {
            Destroy(gameObject);
        }
    }
}