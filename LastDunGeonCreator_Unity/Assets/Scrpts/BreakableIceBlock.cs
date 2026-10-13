
using UnityEngine;

public class BreakableIceBlock : MonoBehaviour
{
    [Header("‰ó‚ê‚½ó‘Ô‚Ì‰æ‘œ")]
    [SerializeField]
    private Sprite brokenSprite;

    [Header("‰æ‘œ•\¦")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private bool isBroken = false;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryBreak(collision.collider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryBreak(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryBreak(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryBreak(other);
    }

    private void TryBreak(Collider2D other)
    {
        if (isBroken)
            return;

        PlayerState state =
            other.GetComponentInParent<PlayerState>();

        if (state == null || !state.IsBurning())
            return;

        isBroken = true;

        // ‰ó‚ê‚½‰æ‘œ‚ÉØ‚è‘Ö‚¦‚é
        if (spriteRenderer != null && brokenSprite != null)
            spriteRenderer.sprite = brokenSprite;

        // •X‚Ì“–‚½‚è”»’è‚ğ‚È‚­‚·
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;
    }
}
