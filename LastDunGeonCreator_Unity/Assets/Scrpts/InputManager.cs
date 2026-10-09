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

    /* 前フレームの横入力 */
    private float previousHorizontal;

    /* 前フレームの縦入力 */
    private float previousVertical;


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
        /* 現在の入力を取得 */
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        /* 前フレームの入力を保存 */
        previousHorizontal = horizontal;
        previousVertical = vertical;
    }


    /*=========================
     * 上
     *=========================*/

    public bool Up()
    {
        /* W */
        if (Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }

        /* ↑ */
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return true;
        }

        /* コントローラー上 */
        if (previousVertical <= -stickThreshold &&
            Input.GetAxisRaw("Vertical") > -stickThreshold)
        {
            return true;
        }

        return false;
    }

    /*=========================
     * 下
     *=========================*/

    public bool Down()
    {
        /* S */
        if (Input.GetKeyDown(KeyCode.S))
        {
            return true;
        }

        /* ↓ */
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * 左
     *=========================*/

    public bool Left()
    {
        /* A */
        if (Input.GetKeyDown(KeyCode.A))
        {
            return true;
        }

        /* ← */
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return true;
        }

        return false;
    }


    /*=========================
     * 右
     *=========================*/

    public bool Right()
    {
        /* D */
        if (Input.GetKeyDown(KeyCode.D))
        {
            return true;
        }

        /* → */
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return true;
        }

        return false;
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