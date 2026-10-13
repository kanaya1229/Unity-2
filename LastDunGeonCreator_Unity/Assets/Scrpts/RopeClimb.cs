
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RopeClimb : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player == null)
            return;

        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.W))
        {
            player.EnterRope(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player != null)
            player.ExitRope(this);
    }
}
