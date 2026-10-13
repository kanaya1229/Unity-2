
/******************************************************************************
 * @file    PlayerController.cs
 * @brief   勇者操作管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class PlayerController
 * @brief 勇者の移動・ジャンプ・アニメーションを管理する
 ******************************************************************************/
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /*=========================
     * 移動設定
     *=========================*/

    [Header("移動速度")]
    [SerializeField]
    private float moveSpeed = 5.0f;

    [Header("ジャンプ力")]
    [SerializeField]
    private float jumpPower = 10.0f;

    [Header("身軽状態のジャンプ力")]
    [SerializeField]
    private float lightJumpPower = 15.0f;

    /* 身軽状態 */
    private bool isLight = false;


    /*=========================
     * 壁判定
     *=========================*/

    [Header("壁レイヤー")]
    [SerializeField]
    private LayerMask wallLayer;

    private bool isWallLeft = false;
    private bool isWallRight = false;


    /*=========================
     * 接地判定
     *=========================*/

    [Header("足元の接地判定")]
    [SerializeField]
    private PlayerGroundCheck groundCheck;

    private bool isGrounded = false;


    /*=========================
     * コンポーネント
     *=========================*/

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float groundGraceTime = 0.08f;
    private float lastGroundedTime = -100f;

    [Header("ロープ")]
    [SerializeField] private float climbSpeed = 3.0f;

    private RopeClimb currentRope;
    private bool isClimbing = false;
    private float normalGravity;


    /*=========================
     * 初期化
     *=========================*/

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        normalGravity = rb.gravityScale;

        animator = GetComponentInChildren<Animator>();

        spriteRenderer =
            GetComponentInChildren<SpriteRenderer>();

        if (groundCheck == null)
        {
            groundCheck =
                GetComponentInChildren<PlayerGroundCheck>();
        }

        if (groundCheck == null)
        {
            Debug.LogError(
                "PlayerGroundCheckが見つかりません！",
                this
            );
        }
    }


    /*=========================
     * 更新
     *=========================*/

    void Update()
    {
        if (PauseManager.IsPaused || InputManager.Instance == null)
            return;

        isGrounded = groundCheck != null &&
                     groundCheck.IsGrounded;

        if (isClimbing)
        {
            Climb();
            UpdateAnimation();
            return;
        }

        Move();
        Jump();
        UpdateAnimation();
    }

    /*=========================
     * 移動
     *=========================*/

    void Move()
    {
        float horizontal = 0.0f;

        if (InputManager.Instance.LeftHold())
        {
            horizontal = -1.0f;
        }

        if (InputManager.Instance.RightHold())
        {
            horizontal = 1.0f;
        }

        /* 壁に向かう移動を禁止 */
        if (horizontal < 0.0f && isWallLeft)
        {
            horizontal = 0.0f;
        }

        if (horizontal > 0.0f && isWallRight)
        {
            horizontal = 0.0f;
        }

        /* 横移動 */
        rb.velocity = new Vector2(
            horizontal * moveSpeed,
            rb.velocity.y
        );

        /* 画像だけ左右反転 */
        if (spriteRenderer != null)
        {
            if (horizontal > 0.01f)
            {
                spriteRenderer.flipX = false;
            }
            else if (horizontal < -0.01f)
            {
                spriteRenderer.flipX = true;
            }
        }
    }


    /*=========================
     * ジャンプ
     *=========================*/

    void Jump()
    {
        if (!isGrounded)
            return;

        if (!InputManager.Instance.Jump())
            return;

        float currentJumpPower =
            isLight ? lightJumpPower : jumpPower;

        rb.velocity = new Vector2(
            rb.velocity.x,
            currentJumpPower
        );

        isGrounded = false;
    }


    /*=========================
     * アニメーション更新
     *=========================*/

    void UpdateAnimation()
    {
        if (animator == null)
            return;

        if (isGrounded)
            lastGroundedTime = Time.time;

        bool animationGrounded =
            isGrounded ||
            Time.time - lastGroundedTime < groundGraceTime;

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsGrounded", animationGrounded);
    }


    /*=========================
     * 壁判定
     *=========================*/

    void OnCollisionStay2D(Collision2D collision)
    {
        bool isWall =
            ((1 << collision.gameObject.layer) & wallLayer) != 0;

        if (!isWall)
            return;

        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact =
                collision.GetContact(i);

            if (contact.normal.x > 0.5f)
            {
                isWallLeft = true;
            }

            if (contact.normal.x < -0.5f)
            {
                isWallRight = true;
            }
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        bool isWall =
            ((1 << collision.gameObject.layer) & wallLayer) != 0;

        if (!isWall)
            return;

        isWallLeft = false;
        isWallRight = false;
    }


    /*=========================
     * ジャンプ台効果
     *=========================*/

    public void ApplyLight()
    {
        isLight = true;
    }

    public void ClearLight()
    {
        isLight = false;
    }


    /*=========================
     * ロープ移動
     *=========================*/

    public void EnterRope(RopeClimb rope)
    {
        currentRope = rope;
        isClimbing = true;

        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }

    public void ExitRope(RopeClimb rope)
    {
        if (currentRope != rope)
            return;

        StopClimbing();
    }

    void StopClimbing()
    {
        currentRope = null;
        isClimbing = false;

        rb.gravityScale = normalGravity;
    }



    void Climb()
    {
        if (InputManager.Instance == null)
            return;

        float vertical = 0f;
        float horizontal = 0f;

        // 上下移動
        if (InputManager.Instance.UpHold())
            vertical = 1f;
        else if (InputManager.Instance.DownHold())
            vertical = -1f;

        // 左右移動
        if (InputManager.Instance.LeftHold())
            horizontal = -1f;
        else if (InputManager.Instance.RightHold())
            horizontal = 1f;

        rb.velocity = new Vector2(
            horizontal * moveSpeed,
            vertical * climbSpeed
        );

        // ジャンプでロープから離れる
        if (InputManager.Instance.Jump())
        {
            StopClimbing();

            rb.velocity = new Vector2(
                rb.velocity.x,
                jumpPower
            );
        }
    }



    public bool IsLight()
    {
        return isLight;
    }
}
