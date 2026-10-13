
using UnityEngine;

public class StageObjectUI : MonoBehaviour
{
    [Header("表示用SpriteRenderer")]
    [SerializeField] private SpriteRenderer objectRenderer;

    [Header("統一する表示サイズ（ワールド単位）")]
    [SerializeField] private Vector2 displaySize = new Vector2(0.5f, 0.5f);

    // 元のスケール
    private Vector3 originalScale;

    void Awake()
    {
        if (objectRenderer == null)
            objectRenderer = GetComponent<SpriteRenderer>();

        if (objectRenderer != null)
            originalScale = objectRenderer.transform.localScale;
    }

    public void SetObjectSprite(Sprite sprite)
    {
        if (objectRenderer == null || sprite == null)
            return;

        objectRenderer.sprite = sprite;

        // Sprite本来のサイズを取得
        Vector2 spriteSize = sprite.bounds.size;

        if (spriteSize.x <= 0 || spriteSize.y <= 0)
            return;

        // 縦横比を維持して指定サイズ内に収める
        float scale = Mathf.Min(
            displaySize.x / spriteSize.x,
            displaySize.y / spriteSize.y
        );

        objectRenderer.transform.localScale =
            new Vector3(
                originalScale.x * scale,
                originalScale.y * scale,
                originalScale.z
            );
    }
}
