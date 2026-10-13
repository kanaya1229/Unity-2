
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckPlayer(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckPlayer(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController controller =
            collision.collider.GetComponentInParent<PlayerController>();

        if (controller != null)
        {
            controller.ClearLight();
        }
    }

    private void CheckPlayer(Collision2D collision)
    {
        PlayerController controller =
            collision.collider.GetComponentInParent<PlayerController>();

        if (controller == null)
            return;

        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);

            // ƒWƒƒƒ“ƒv‘ä‚Ìã‚Éæ‚Á‚Ä‚¢‚é
            if (contact.normal.y < -0.5f)
            {
                controller.ApplyLight();
                return;
            }
        }
    }
}
