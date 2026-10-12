/******************************************************************************
 * @file    PlayerController.cs
 * @brief   勇者操作管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class PlayerController
 * @brief 勇者の移動・ジャンプを管理する
 ******************************************************************************/
public class PlayerController : MonoBehaviour
{
    /*=========================
     * 移動設定
     *=========================*/

    /* 移動速度 */
    [SerializeField]
    private float moveSpeed = 5.0f;

    /* ジャンプ力 */
    [SerializeField]
    private float jumpPower = 10.0f;



    /* 身軽状態のジャンプ力 */
    [SerializeField]
    private float lightJumpPower = 15.0f;

    /* 身軽状態か */
    private bool isLight = false;

    /*=========================
     * 壁判定
     *=========================*/

    /* 壁レイヤー */
    [SerializeField]
    private LayerMask wallLayer;

    /* 左側に壁がある */
    private bool isWallLeft;

    /* 右側に壁がある */
    private bool isWallRight;


    /*=========================
     * 接地判定
     *=========================*/

    /* 地面レイヤー */
    [SerializeField]
    private LayerMask groundLayer;

    /* 接地中 */
    private bool isGrounded;


    /*=========================
     * コンポーネント
     *=========================*/

    private Rigidbody2D rb;


    /*=========================
     * 初期化
     *=========================*/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    /*=========================
     * 更新
     *=========================*/

    void Update()
    {
        Move();
        Jump();
    }


    /*=========================
     * 移動
     *=========================*/

    void Move()
    {
        float horizontal = 0.0f;

        /* 左入力 */
        if (InputManager.Instance.LeftHold())
        {
            horizontal = -1.0f;
        }

        /* 右入力 */
        if (InputManager.Instance.RightHold())
        {
            horizontal = 1.0f;
        }


        /*=========================
         * 壁に向かう移動を禁止
         *=========================*/

        /* 左の壁に当たっている */
        if (horizontal < 0.0f && isWallLeft)
        {
            horizontal = 0.0f;
        }

        /* 右の壁に当たっている */
        if (horizontal > 0.0f && isWallRight)
        {
            horizontal = 0.0f;
        }


        /* 横移動 */
        rb.velocity = new Vector2(
            horizontal * moveSpeed,
            rb.velocity.y
        );
    }


    /*=========================
     * ジャンプ
     *=========================*/

    void Jump()
    {
        /* 空中ではジャンプ不可 */
        if (!isGrounded)
        {
            return;
        }

        /* ジャンプ入力 */
        if (InputManager.Instance.Jump())
        {

            float currentJumpPower = isLight
                ? lightJumpPower
                : jumpPower;

            rb.velocity = new Vector2(
                rb.velocity.x,
                currentJumpPower
            );

            isGrounded = false;
        }
    }


    /*=========================
     * 壁・地面判定
     *=========================*/

    void OnCollisionStay2D(Collision2D collision)
    {
        /*=========================
         * レイヤー確認
         *=========================*/

        bool isGround =
            ((1 << collision.gameObject.layer) & groundLayer) != 0;

        bool isWall =
            ((1 << collision.gameObject.layer) & wallLayer) != 0;


        /*=========================
         * 接触面を確認
         *=========================*/

        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);

            /*=========================
             * 地面
             *=========================*/

            if (isGround && contact.normal.y > 0.5f)
            {
                isGrounded = true;
            }


            /*=========================
             * 左右の壁
             *=========================*/

            if (isWall)
            {
                /* 左側の壁 */
                if (contact.normal.x > 0.5f)
                {
                    isWallLeft = true;
                }

                /* 右側の壁 */
                if (contact.normal.x < -0.5f)
                {
                    isWallRight = true;
                }
            }
        }
    }


    /*=========================
     * 接触終了
     *=========================*/

    void OnCollisionExit2D(Collision2D collision)
    {
        /* 地面 */
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }

        /* 壁 */
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            isWallLeft = false;
            isWallRight = false;
        }
    }


    /* ジャンプ台の効果 */
    public void ApplyLight()
    {
        isLight = true;
    }

    /* 通常状態へ戻す */
    public void ClearLight()
    {
        isLight = false;
    }
}