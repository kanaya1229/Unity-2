/******************************************************************************
 * @file    StageObjectData.cs
 * @brief   ステージ配置オブジェクトデータ
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class StageObjectData
 * @brief ステージに配置するオブジェクトのデータ
 ******************************************************************************/
[System.Serializable]
public class StageObjectData
{
    /*=========================
     * オブジェクト情報
     *=========================*/

    /* オブジェクト名 */
    public string objectName;

    /* 配置するプレハブ */
    public GameObject prefab;

    /* 最大個数 */
    public int maxCount = 4;

    /* 残り個数 */
    [HideInInspector]
    public int remainCount;
}