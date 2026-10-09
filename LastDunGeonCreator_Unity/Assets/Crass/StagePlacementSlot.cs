/******************************************************************************
 * @file    StagePlacementSlot.cs
 * @brief   ステージ配置位置管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class StagePlacementSlot
 * @brief ステージ上の1つの配置場所を管理する
 ******************************************************************************/
public class StagePlacementSlot : MonoBehaviour
{
    /*=========================
     * 移動先
     *=========================*/

    /* 上 */
    public StagePlacementSlot up;

    /* 下 */
    public StagePlacementSlot down;

    /* 左 */
    public StagePlacementSlot left;

    /* 右 */
    public StagePlacementSlot right;


    /*=========================
     * 表示
     *=========================*/

    /* 選択カーソル */
    public GameObject selectArrow;

    /* 現在配置されているオブジェクト */
    public GameObject placedObject;


    /*=========================
     * 選択表示
     *=========================*/

    public void SetSelect(bool value)
    {
        if (selectArrow != null)
        {
            selectArrow.SetActive(value);
        }
    }


    /*=========================
     * オブジェクト配置
     *=========================*/

    public void PlaceObject(GameObject prefab)
    {
        /* すでに配置されている場合は削除 */
        RemoveObject();

        /* オブジェクト生成 */
        placedObject = Instantiate(prefab, transform);

        /* 配置位置を中央にする */
        placedObject.transform.localPosition = Vector3.zero;

        /* 回転を初期化 */
        placedObject.transform.localRotation = Quaternion.identity;
    }


    /*=========================
     * オブジェクト削除
     *=========================*/

    public void RemoveObject()
    {
        if (placedObject != null)
        {
            Destroy(placedObject);
            placedObject = null;
        }
    }


    /*=========================
     * 配置確認
     *=========================*/

    public bool IsPlaced()
    {
        return placedObject != null;
    }
}