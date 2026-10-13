
/******************************************************************************
 * @file    PlayerGroundCheck.cs
 * @brief   勇者の足元接地判定クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class PlayerGroundCheck
 * @brief 足元のColliderで地面との接触を判定する
 ******************************************************************************/
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerGroundCheck : MonoBehaviour
{
    /*=========================
     * コンポーネント
     *=========================*/

    private BoxCollider2D groundCollider;

    /* 接触結果 */
    private readonly Collider2D[] results =
        new Collider2D[16];

    private ContactFilter2D contactFilter;


    /*=========================
     * 接地状態
     *=========================*/

    public bool IsGrounded { get; private set; }


    /*=========================
     * 初期化
     *=========================*/

    void Awake()
    {
        groundCollider = GetComponent<BoxCollider2D>();

        /* 足元ColliderはTrigger */
        groundCollider.isTrigger = true;

        /* Trigger同士の接触は無視 */
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;
        contactFilter.useLayerMask = false;
        contactFilter.useDepth = false;
    }


    /*=========================
     * 接地判定
     *=========================*/

    void FixedUpdate()
    {
        IsGrounded = false;

        int count = groundCollider.OverlapCollider(
            contactFilter,
            results
        );

        for (int i = 0; i < count; i++)
        {
            Collider2D other = results[i];

            if (other == null)
                continue;

            /* 自分自身のColliderは除外 */
            if (other.transform.root == transform.root)
                continue;

            /* Triggerは除外 */
            if (other.isTrigger)
                continue;

            IsGrounded = true;
            break;
        }
    }


    /*=========================
     * デバッグ表示
     *=========================*/

    void OnDrawGizmos()
    {
        BoxCollider2D box =
            GetComponent<BoxCollider2D>();

        if (box == null)
            return;

        Gizmos.color =
            IsGrounded ? Color.green : Color.red;

        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawWireCube(
            box.offset,
            box.size
        );
    }
}
