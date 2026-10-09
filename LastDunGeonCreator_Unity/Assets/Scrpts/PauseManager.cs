/******************************************************************************
 * @file    PauseManager.cs
 * @brief   ポーズ画面管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/******************************************************************************
 * @class PauseManager
 * @brief ポーズ画面・設定画面を管理する
 ******************************************************************************/
public class PauseManager : MonoBehaviour
{
    /*=========================
     * UI
     *=========================*/

    /* ポーズ画面 */
    [SerializeField]
    private GameObject pausePanel;

    /* 設定画面 */
    [SerializeField]
    private GameObject settingPanel;

    /* 音量スライダー */
    [SerializeField]
    private Slider volumeSlider;


    /*=========================
     * 内部変数
     *=========================*/

    /* ポーズ中か */
    private bool isPaused = false;


    /*=========================
     * 初期化
     *=========================*/

    void Start()
    {
        /* 最初はポーズ画面を非表示 */
        pausePanel.SetActive(false);

        /* 最初は設定画面を非表示 */
        settingPanel.SetActive(false);

        /* 時間を通常に戻す */
        Time.timeScale = 1.0f;

        /* AudioManagerから現在の音量を取得 */
        if (AudioManager.Instance != null)
        {
            volumeSlider.value = AudioManager.Instance.GetVolume();
        }
    }


    /*=========================
     * 更新処理
     *=========================*/

    void Update()
    {
        /* ESCキーでポーズ切り替え */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    /*=========================
     * ポーズ開始
     *=========================*/

    public void PauseGame()
    {
        isPaused = true;

        /* ポーズ画面を表示 */
        pausePanel.SetActive(true);

        /* 設定画面を閉じる */
        settingPanel.SetActive(false);

        /* ゲームを停止 */
        Time.timeScale = 0.0f;
    }


    /*=========================
     * ポーズ解除
     *=========================*/

    public void ResumeGame()
    {
        isPaused = false;

        /* ポーズ画面を非表示 */
        pausePanel.SetActive(false);

        /* 設定画面を非表示 */
        settingPanel.SetActive(false);

        /* ゲームを再開 */
        Time.timeScale = 1.0f;
    }


    /*=========================
     * 設定画面を開く
     *=========================*/

    public void OpenSetting()
    {
        /* ポーズ画面を非表示 */
        pausePanel.SetActive(false);

        /* 設定画面を表示 */
        settingPanel.SetActive(true);

        /* 現在の音量をスライダーに反映 */
        if (AudioManager.Instance != null)
        {
            volumeSlider.value = AudioManager.Instance.GetVolume();
        }
    }


    /*=========================
     * 設定画面を閉じる
     *=========================*/

    public void CloseSetting()
    {
        /* 設定画面を非表示 */
        settingPanel.SetActive(false);

        /* ポーズ画面を表示 */
        pausePanel.SetActive(true);
    }


    /*=========================
     * 音量設定
     *=========================*/

    public void ChangeVolume()
    {
        /* AudioManagerが存在する場合 */
        if (AudioManager.Instance != null)
        {
            /* スライダーの値をAudioManagerへ渡す */
            AudioManager.Instance.SetVolume(volumeSlider.value);
        }
    }


    /*=========================
     * タイトルに戻る
     *=========================*/

    public void ReturnToTitle()
    {
        /* 時間を通常に戻す */
        Time.timeScale = 1.0f;

        /* ポーズ解除 */
        isPaused = false;

        /* タイトル画面へ */
        SceneManager.LoadScene("StartScene");
    }


    /*=========================
     * ゲーム終了
     *=========================*/

    public void QuitGame()
    {
        /* 時間を通常に戻す */
        Time.timeScale = 1.0f;

#if UNITY_EDITOR

        /* Unityエディタでは再生停止 */
        UnityEditor.EditorApplication.isPlaying = false;

#else

        /* ゲームを終了 */
        Application.Quit();

#endif
    }
}