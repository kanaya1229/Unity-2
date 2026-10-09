/******************************************************************************
 * @file    AudioManager.cs
 * @brief   ゲーム全体の音量管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class AudioManager
 * @brief ゲーム全体の音量を管理する
 ******************************************************************************/
public class AudioManager : MonoBehaviour
{
    /*=========================
     * シングルトン
     *=========================*/

    public static AudioManager Instance;


    /*=========================
     * 音量
     *=========================*/

    /* ゲーム全体の音量 */
    [SerializeField]
    private float volume = 1.0f;


    /*=========================
     * 初期化
     *=========================*/

    void Awake()
    {
        /* すでにAudioManagerが存在する場合 */
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        /* 自分自身を登録 */
        Instance = this;

        /* シーンが変わっても残す */
        DontDestroyOnLoad(gameObject);

        /* 音量を設定 */
        AudioListener.volume = volume;
    }


    /*=========================
     * 音量変更
     *=========================*/

    public void SetVolume(float value)
    {
        /* 音量を保存 */
        volume = value;

        /* ゲーム全体の音量を変更 */
        AudioListener.volume = volume;
    }


    /*=========================
     * 音量取得
     *=========================*/

    public float GetVolume()
    {
        return volume;
    }
}