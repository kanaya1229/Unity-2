using UnityEngine;

public class FireHazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("‰Š‚ÉÚG: " + other.name);

        PlayerState state =
            other.GetComponentInParent<PlayerState>();

        if (state == null)
        {
            Debug.LogWarning("PlayerState‚ªŒ©‚Â‚©‚ç‚È‚¢");
            return;
        }

        state.ApplyFire();

        Debug.Log("”RÄˆ—‚ğÀs");
    }
}