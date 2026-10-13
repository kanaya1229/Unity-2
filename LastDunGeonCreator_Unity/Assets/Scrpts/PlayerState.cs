/******************************************************************************
 * @file    PlayerSteta.cs
 * @brief   プレイヤーステータス管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
 * @class PlayerSteta
 * @brief プレイヤーのHP・状態異常を管理する
 ******************************************************************************/
public class PlayerState : MonoBehaviour
{
    /*=========================
     * HP
     *=========================*/

    /* 最大HP */
    [SerializeField]
    private int maxHP = 100000;

    /* 現在HP */
    [SerializeField]
    private int currentHP = 100000;


    /*=========================
     * HP回復
     *=========================*/

    /* 自動回復量 */
    [SerializeField]
    private int recoveryAmount = 1;

    /* 自動回復間隔 */
    [SerializeField]
    private float recoveryInterval = 1.0f;


    /*=========================
     * 状態異常
     *=========================*/

    /* 状態異常中 */
    private bool isAbnormal = false;

    /* 状態異常の残り時間 */
    private float abnormalTime = 0.0f;


    /*=========================
     * 初期化
     *=========================*/

    void Start()
    {
        /* HPを最大値にする */
        currentHP = maxHP;

        /* 自動回復開始 */
        StartCoroutine(Recovery());
    }


    /*=========================
     * 更新
     *=========================*/

    void Update()
    {
        /* 状態異常の時間処理 */
        UpdateAbnormal();
    }


    /*=========================
     * ダメージ
     *=========================*/

    public void TakeDamage()
    {
        /* HPが1ならそれ以上減らさない */
        if (currentHP <= 1)
        {
            currentHP = 1;
            return;
        }

        /* 受けるダメージは1固定 */
        currentHP -= 1;

        /* HPが1未満にならないようにする */
        if (currentHP < 1)
        {
            currentHP = 1;
        }
    }


    /*=========================
     * 自動回復
     *=========================*/

    IEnumerator Recovery()
    {
        while (true)
        {
            /* 回復間隔 */
            yield return new WaitForSeconds(recoveryInterval);

            /* HP回復 */
            currentHP += recoveryAmount;

            /* 最大HPを超えない */
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }


    /*=========================
     * 状態異常
     *=========================*/

    public void SetAbnormal(float duration)
    {
        /* 状態異常開始 */
        isAbnormal = true;

        /* 効果時間を設定 */
        abnormalTime = duration;
    }


    /* 状態異常の時間処理 */
    void UpdateAbnormal()
    {
        /* 状態異常でなければ何もしない */
        if (!isAbnormal)
        {
            return;
        }

        /* 時間を減らす */
        abnormalTime -= Time.deltaTime;

        /* 時間切れ */
        if (abnormalTime <= 0.0f)
        {
            abnormalTime = 0.0f;
            isAbnormal = false;
        }
    }


    /*=========================
     * HP取得
     *=========================*/

    public int GetHP()
    {
        return currentHP;
    }


    /* 最大HP取得 */
    public int GetMaxHP()
    {
        return maxHP;
    }


    /*=========================
     * 状態異常取得
     *=========================*/

    public bool IsAbnormal()
    {
        return isAbnormal;
    }


    /* 状態異常残り時間取得 */
    public float GetAbnormalTime()
    {
        return abnormalTime;
    }


    /* 炎上などの状態異常を開始 */
    public void ApplyFire()
    {
        TakeDamage();
        SetAbnormal(20.0f);
    }

    /* 炎上状態かどうか */
    public bool IsBurning()
    {
        return IsAbnormal();
    }
}