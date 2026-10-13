
using System.Collections;
using UnityEngine;

public class HeroEffectController : MonoBehaviour
{
    [Header("主人公の設定")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float normalJumpForce = 7f;
    [SerializeField] private float boostedJumpForce = 12f;

    [Header("状態")]
    [SerializeField] private float fireDuration = 20f;

    public bool IsBurning { get; private set; }
    public bool IsLight { get; private set; }
    public int KeyCount { get; private set; }

    private Coroutine fireCoroutine;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // ダメージ処理：実際のHP処理は既存のHPシステムと接続する
    public void TakeDamage(int damage = 1)
    {
        Debug.Log("主人公がダメージを受けた: " + damage);

        // TODO: 既存のHP管理処理に接続する
    }

    // 炎上状態を20秒間適用
    public void ApplyFire()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }

        fireCoroutine = StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        IsBurning = true;
        Debug.Log("主人公が炎上状態になった");

        yield return new WaitForSeconds(fireDuration);

        IsBurning = false;
        fireCoroutine = null;
        Debug.Log("炎上状態が終了した");
    }

    // 身軽状態を適用
    public void ApplyLight()
    {
        IsLight = true;
        Debug.Log("主人公が身軽状態になった");
    }

    public void ClearLight()
    {
        IsLight = false;
    }

    // ジャンプ台から呼び出す
    public void BoostJump()
    {
        if (rb == null)
        {
            return;
        }

        Vector2 velocity = rb.velocity;
        velocity.y = 0f;
        rb.velocity = velocity;

        rb.AddForce(
            Vector2.up * boostedJumpForce,
            ForceMode2D.Impulse
        );

        ApplyLight();
    }

    public float GetJumpForce()
    {
        return IsLight ? boostedJumpForce : normalJumpForce;
    }

    public void AddKey()
    {
        KeyCount++;
        Debug.Log("キー取得。所持数: " + KeyCount);
    }
}