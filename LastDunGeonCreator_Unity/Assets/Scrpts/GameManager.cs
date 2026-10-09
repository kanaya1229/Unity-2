/******************************************************************************
 * @file    GameManager.cs
 * @brief   ゲーム全体の情報を管理する
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class GameManager
 * @brief シーン間で引き継ぐゲーム情報を管理する
 ******************************************************************************/
public class GameManager : MonoBehaviour
{
    /*=========================
     * インスタンス
     *=========================*/

    public static GameManager Instance;


    /*=========================
     * 引き継ぎデータ
     *=========================*/

    /* 鍵の入手数 */
    private int keyCount = 0;


    /*=========================
     * 初期化
     *=========================*/

    void Awake()
    {
        /* 既に存在する場合 */
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        /* シーンをまたいでも残す */
        DontDestroyOnLoad(gameObject);
    }


    /*=========================
     * 鍵
     *=========================*/

    /* 鍵を取得 */
    public void AddKey(int amount = 1)
    {
        keyCount += amount;

        /* 0未満にはしない */
        if (keyCount < 0)
        {
            keyCount = 0;
        }
    }


    /* 鍵を使用 */
    public bool UseKey(int amount = 1)
    {
        /* 足りない場合 */
        if (keyCount < amount)
        {
            return false;
        }

        keyCount -= amount;

        return true;
    }


    /* 鍵の入手数を取得 */
    public int GetKeyCount()
    {
        return keyCount;
    }


    /*=========================
     * 追加用
     *=========================*/

    /*
     * 今後、引き継ぐデータを追加する場合は
     * ここに変数と関数を追加する。
     *
     * 例：
     *
     * private int xxxCount = 0;
     *
     * public void AddXxx(int amount = 1)
     * {
     *     xxxCount += amount;
     * }
     *
     * public int GetXxxCount()
     * {
     *     return xxxCount;
     * }
     */


    /*=========================
     * データリセット
     *=========================*/

    public void ResetData()
    {
        keyCount = 0;

        /*
         * 今後追加する引き継ぎデータも
         * ここで初期化する。
         */
    }
}