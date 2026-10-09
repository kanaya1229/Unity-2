/******************************************************************************
 * @file    OperationManager.cs
 * @brief   操作方法画面管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class OperationManager
 * @brief 操作方法画面を管理する
 ******************************************************************************/
public class OperationManager : MonoBehaviour
{
    /*=========================
     * UI
     *=========================*/

    /* 操作方法画面 */
    [SerializeField]
    private GameObject operationPanel;


    /*=========================
     * 初期化
     *=========================*/

    void Start()
    {
        /* 最初は非表示 */
        operationPanel.SetActive(false);
    }


    /*=========================
     * 操作方法を開く
     *=========================*/

    public void OpenOperation()
    {
        operationPanel.SetActive(true);
    }


    /*=========================
     * 操作方法を閉じる
     *=========================*/

    public void CloseOperation()
    {
        operationPanel.SetActive(false);
    }
}