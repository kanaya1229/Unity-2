/******************************************************************************
 * @file    InputManager.cs
 * @brief   ゲーム全体の入力管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class InputManager
 * @brief キーボード・コントローラーの入力を管理する
 ******************************************************************************/
public class InputManager : MonoBehaviour
{
    /*=========================
     * シングルトン
     *=========================*/

    public static InputManager Instance;


    /*=========================
     * コントローラー設定
     *=========================*/

    /* Aボタン */
    [SerializeField]
    private KeyCode controllerA = KeyCode.JoystickButton0;

    /* Bボタン */
    [SerializeField]
    private KeyCode controllerB = KeyCode.JoystickButton1;

    /* STARTボタン */
    [SerializeField]
    private KeyCode controllerStart = KeyCode.JoystickButton7;


    /*=========================
     * スティック設定
     *=========================*/

    /* スティック入力判定値 */
    [SerializeField]
    private float stickThreshold = 0.5f;


    /*=========================
     * 内部変数
     *=========================*/

    private bool previousUp;
    private bool previousDown;
    private bool previousLeft;
    private bool previousRight;

    private bool upPressed;
    private bool downPressed;
    private bool leftPressed;
    private bool rightPressed;

    /*=========================
     * 初期化
     *=========================*/

    void Awake()
    {
        /* すでに存在する場合 */
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        /* シングルトン登録 */
        Instance = this;

        /* シーンをまたいで使用 */
        DontDestroyOnLoad(gameObject);
    }


    /*=========================
     * 更新処理
     *=========================*/

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool up = vertical > stickThreshold;
        bool down = vertical < -stickThreshold;
        bool left = horizontal < -stickThreshold;
        bool right = horizontal > stickThreshold;

        upPressed = up && !previousUp;
        downPressed = down && !previousDown;
        leftPressed = left && !previousLeft;
        rightPressed = right && !previousRight;

        previousUp = up;
        previousDown = down;
        previousLeft = left;
        previousRight = right;
    }
    public bool Up()
    {
        return Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow)
            || upPressed;
    }

    public bool Down()
    {
        return Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || downPressed;
    }

    public bool Left()
    {
        return Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || leftPressed;
    }

    public bool Right()
    {
        return Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.RightArrow)
            || rightPressed;
    }

    /*=========================
 * 左入力継続
 *=========================*/

    public bool LeftHold()
    {
        /* A */
        if (Input.GetKey(KeyCode.A))
        {
            return true;
        }

        /* ← */
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * 右入力継続
     *=========================*/

    public bool RightHold()
    {
        /* D */
        if (Input.GetKey(KeyCode.D))
        {
            return true;
        }

        /* → */
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * 上入力継続
     *=========================*/

    public bool UpHold()
    {
        /* W */
        if (Input.GetKey(KeyCode.W))
        {
            return true;
        }

        /* ↑ */
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * 下入力継続
     *=========================*/

    public bool DownHold()
    {
        /* S */
        if (Input.GetKey(KeyCode.S))
        {
            return true;
        }

        /* ↓ */
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * 決定
     *=========================*/

    public bool Submit()
    {
        /* Enter */
        if (Input.GetKeyDown(KeyCode.Return))
        {
            return true;
        }

        /* テンキーEnter */
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            return true;
        }

        /* コントローラー B */
        if (Input.GetKeyDown(controllerB))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * 戻る
     *=========================*/

    public bool Back()
    {
        /* Space */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        /* コントローラー A */
        if (Input.GetKeyDown(controllerA))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * ポーズ
     *=========================*/

    public bool Pause()
    {
        /* Esc */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }

        /* コントローラー START */
        if (Input.GetKeyDown(controllerStart))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * ジャンプ
     *=========================*/

    public bool Jump()
    {
        /* Space */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        /* コントローラー A */
        if (Input.GetKeyDown(controllerA))
        {
            return true;
        }

        return false;
    }
}