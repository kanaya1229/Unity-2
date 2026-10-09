/******************************************************************************
 * @file    StageCursor.cs
 * @brief   ステージ配置カーソル
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class   StageCursor
 * @brief   配置位置を示すカーソル
 ******************************************************************************/
public class StageCursor : MonoBehaviour
{
    /* カーソルの移動 */
    public void SetPosition(Transform target)
    {
        transform.position = target.position;
    }
}