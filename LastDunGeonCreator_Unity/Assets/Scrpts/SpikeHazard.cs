
using UnityEngine;

public class SpikeHazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerState state =
            other.GetComponentInParent<PlayerState>();

        if (state != null)
        {
            state.TakeDamage();
        }
    }
}