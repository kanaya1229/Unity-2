
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerState state =
            other.GetComponentInParent<PlayerState>();

        if (state != null)
        {
            state.ApplyFire();
        }
    }
}