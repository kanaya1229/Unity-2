
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController controller =
            collision.collider.GetComponentInParent<PlayerController>();

        if (controller == null)
        {
            return;
        }

        if (collision.contactCount > 0 &&
            collision.GetContact(0).normal.y > 0.5f)
        {
            controller.ApplyLight();
        }
    }
}