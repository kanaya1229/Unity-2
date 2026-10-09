/******************************************************************************
 * @file    TitleAnimation.cs
 * @brief   タイトル画面アニメーション管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class TitleAnimation
 * @brief タイトル画面の動きを管理する
 ******************************************************************************/
public class TitleAnimation : MonoBehaviour
{
    /*=========================
     * UI
     *=========================*/

    /* 動かすロゴ */
    [SerializeField]
    private RectTransform logo;


    /*=========================
     * アニメーション設定
     *=========================*/

    /* 何秒ごとに動くか */
    [SerializeField]
    private float moveInterval = 0.2f;

    /* 1回の移動量 */
    [SerializeField]
    private float movePixel = 0.01f;


    /*=========================
     * 内部変数
     *=========================*/

    /* 初期位置 */
    private Vector2 startPosition;

    /* タイマー */
    private float timer;

    /* 現在の動き */
    private int step = 0;


    /*=========================
     * 初期化
     *=========================*/

    void Start()
    {
        /* 初期位置を保存 */
        startPosition = logo.anchoredPosition;

        timer = 0.0f;
    }


    /*=========================
     * 更新処理
     *=========================*/

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        /* 一定時間経過するまで待つ */
        if (timer < moveInterval)
        {
            return;
        }

        timer = 0.0f;

        /* 動きを進める */
        step++;

        if (step > 7)
        {
            step = 0;
        }

        float x = 0.0f;
        float y = 0.0f;

        /*
         * 8段階で1マスずつ動く
         *
         *       ↑
         *       │
         *   ←── ● ──→
         *       │
         *       ↓
         */

        switch (step)
        {
            /* 右 */
            case 1:
                x = movePixel;
                break;

            /* 右上 */
            case 2:
                x = movePixel;
                y = movePixel;
                break;

            /* 上 */
            case 3:
                y = movePixel;
                break;

            /* 左上 */
            case 4:
                x = -movePixel;
                y = movePixel;
                break;

            /* 左 */
            case 5:
                x = -movePixel;
                break;

            /* 左下 */
            case 6:
                x = -movePixel;
                y = -movePixel;
                break;

            /* 下 */
            case 7:
                y = -movePixel;
                break;

            /* 0 */
            default:
                x = 0.0f;
                y = 0.0f;
                break;
        }

        /* 初期位置から1マスだけ移動 */
        logo.anchoredPosition =
            startPosition + new Vector2(x, y);
    }
}