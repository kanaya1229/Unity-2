/******************************************************************************
 * @file    EndRoll.cs
 * @brief   エンドロール管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

/******************************************************************************
 * @class EndRoll
 * @brief エンドロールの表示・終了を管理する
 ******************************************************************************/
public class EndRoll : MonoBehaviour
{
    /*=========================
     * インスペクター設定
     *=========================*/

    /* エンドロールの親オブジェクト */
    [SerializeField]
    private RectTransform rollText;

    /* エンドロールの移動速度 */
    [SerializeField]
    private float rollSpeed = 70.0f;

    /* エンドロール終了後の待ち時間 */
    [SerializeField]
    private float endWaitTime = 3.0f;

    /*=========================
     * 内部変数
     *=========================*/

    /* エンドロール終了フラグ */
    private bool isFinished = false;

    /* 待ち時間 */
    private float waitTimer = 0.0f;


    /*=========================
     * 更新処理
     *=========================*/

    void Update()
    {
        /* エンドロール終了後 */
        if (isFinished)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= endWaitTime)
            {
                EndRollFinish();
            }

            return;
        }

        /* エンドロールを上方向へ移動 */
        rollText.anchoredPosition += Vector2.up * rollSpeed * Time.deltaTime;

        /* 画面外まで移動したら終了 */
        if (rollText.anchoredPosition.y >= 3100.0f)
        {
            isFinished = true;
        }
    }


    /*=========================
     * エンドロール終了
     *=========================*/

    private void EndRollFinish()
    {
        /*
         * タイトルへ戻る
         * 必要に応じてシーン名を変更
         */
        SceneManager.LoadScene("StartScene");
    }


    /*=========================
     * スキップ
     *=========================*/

    public void SkipEndRoll()
    {
        EndRollFinish();
    }
}