/******************************************************************************
 * @file    TitleManager.cs
 * @brief   タイトル画面管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/******************************************************************************
 * @class TitleManager
 * @brief タイトル画面のボタン選択を管理する
 ******************************************************************************/
public class TitleManager : MonoBehaviour
{
    /*=========================
     * ボタン
     *=========================*/

    /* タイトルボタン */
    [SerializeField]
    private Image[] buttons;

    /* 選択中の表示 */
    [SerializeField]
    private Image[] selectFrames;

    [Header("タイトルBGM")]
    [SerializeField] private AudioSource titleBGM;

    /*=========================
     * 選択
     *=========================*/

    /* 現在選択しているボタン */
    private int currentIndex = 0;


    /*=========================
     * 更新
     *=========================*/

    void Update()
    {
        Select();

        Submit();
    }


    /*=========================
     * ボタン選択
     *=========================*/

    void Select()
    {
        /* 左 */
        if (InputManager.Instance.Left())
        {
            currentIndex--;

            if (currentIndex < 0)
            {
                currentIndex = buttons.Length - 1;
            }

            UpdateSelectFrame();
        }

        /* 右 */
        if (InputManager.Instance.Right())
        {
            currentIndex++;

            if (currentIndex >= buttons.Length)
            {
                currentIndex = 0;
            }

            UpdateSelectFrame();
        }
    }


    /*=========================
     * 選択表示
     *=========================*/

    void UpdateSelectFrame()
    {
        for (int i = 0; i < selectFrames.Length; i++)
        {
            selectFrames[i].gameObject.SetActive(
                i == currentIndex
            );
        }
    }


    /*=========================
     * 決定
     *=========================*/

    void Submit()
    {
        if (!InputManager.Instance.Submit())
        {
            return;
        }

        /* START */
        if (currentIndex == 0)
        {
            StartGame();
        }

        /* END */
        else if (currentIndex == 1)
        {
            QuitGame();
        }
    }


    /*=========================
     * スタート
     *=========================*/

    public void StartGame()
    {
        // タイトルBGMを停止
        if (titleBGM != null)
        {
            titleBGM.Stop();
        }

        // ステージ1へ移動
        SceneManager.LoadScene("Game1Scene");
    }

    /*=========================
     * ゲーム終了
     *=========================*/

    public void QuitGame()
    {
#if UNITY_EDITOR
        /* Unityエディタ上では再生を停止 */
        UnityEditor.EditorApplication.isPlaying = false;
#else
        /* ビルド後はゲームを終了 */
        Application.Quit();
#endif
    }
}