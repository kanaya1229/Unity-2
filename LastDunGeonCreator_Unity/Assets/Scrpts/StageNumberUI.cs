
using UnityEngine;

public class StageNumberUI : MonoBehaviour
{
    [Header("数字を表示するSpriteRenderer")]
    [SerializeField] private SpriteRenderer numberRenderer;

    [Header("数字画像（0～9の順番）")]
    [SerializeField] private Sprite[] numberSprites = new Sprite[10];

    [Header("統一表示サイズ（ワールド単位）")]
    [SerializeField] private Vector2 displaySize = new Vector2(0.4f, 0.5f);

    private Vector3 baseScale;

    void Awake()
    {
        if (numberRenderer == null)
            numberRenderer = GetComponent<SpriteRenderer>();

        if (numberRenderer != null)
            baseScale = numberRenderer.transform.localScale;
    }

    public void SetNumber(int number)
    {
        if (numberRenderer == null ||
            numberSprites == null ||
            number < 0 ||
            number >= numberSprites.Length ||
            numberSprites[number] == null)
            return;

        Sprite sprite = numberSprites[number];
        numberRenderer.sprite = sprite;

        Vector2 spriteSize = sprite.bounds.size;

        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
            return;

        float scale = Mathf.Min(
            displaySize.x / spriteSize.x,
            displaySize.y / spriteSize.y
        );

        // 表示サイズを統一しつつ縦横比を維持
        numberRenderer.transform.localScale = new Vector3(
            baseScale.x * scale,
            baseScale.y * scale,
            baseScale.z
        );
    }
}
