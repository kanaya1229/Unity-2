/******************************************************************************
 * @file    StageCustomizeManager.cs
 * @brief   ステージカスタマイズ管理クラス
 * @author  Tanaka Kaito
 ******************************************************************************/

using UnityEngine;

/******************************************************************************
 * @class StageCustomizeManager
 * @brief ステージカスタマイズ画面全体を管理する
 ******************************************************************************/
public class StageCustomizeManager : MonoBehaviour
{
    /*=========================
     * 配置場所
     *=========================*/

    /* ステージ上の配置場所 */
    [SerializeField]
    private StagePlacementSlot[] slots;


    /*=========================
     * 配置オブジェクト
     *=========================*/

    /* 選択可能なオブジェクト */
    [SerializeField]
    private StageObjectData[] objects;


    /*=========================
     * オブジェクト選択
     *=========================*/

    /* 現在選択しているオブジェクト */
    private int currentObjectIndex;


    /*=========================
     * 配置場所選択
     *=========================*/

    /* 現在選択している配置場所 */
    private StagePlacementSlot currentSlot;


    /*=========================
     * 状態
     *=========================*/

    private enum CustomizeMode
    {
        ObjectSelect,
        PlacementSelect
    }

    /* 現在のモード */
    private CustomizeMode currentMode;


    /*=========================
     * 初期化
     *=========================*/

    void Start()
    {
        /* オブジェクト個数を初期化 */
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].remainCount = objects[i].maxCount;
        }

        /* 最初のオブジェクト */
        currentObjectIndex = 0;

        /* 最初の配置場所 */
        if (slots.Length > 0)
        {
            currentSlot = slots[0];
        }

        /* オブジェクト選択から開始 */
        currentMode = CustomizeMode.ObjectSelect;

        /* 配置カーソルを更新 */
        UpdateSlotSelect();
    }


    /*=========================
     * 更新
     *=========================*/

    void Update()
    {
        if (InputManager.Instance == null)
        {
            return;
        }

        /* オブジェクト選択 */
        if (currentMode == CustomizeMode.ObjectSelect)
        {
            ObjectSelectUpdate();
        }

        /* 配置場所選択 */
        else if (currentMode == CustomizeMode.PlacementSelect)
        {
            PlacementSelectUpdate();
        }
    }


    /*=========================
     * オブジェクト選択
     *=========================*/

    void ObjectSelectUpdate()
    {
        /* 上 */
        if (InputManager.Instance.Up())
        {
            SelectPreviousObject();
        }

        /* 下 */
        if (InputManager.Instance.Down())
        {
            SelectNextObject();
        }

        /* 決定 */
        if (InputManager.Instance.Submit())
        {
            /* 個数が残っている場合 */
            if (objects[currentObjectIndex].remainCount > 0)
            {
                currentMode = CustomizeMode.PlacementSelect;

                UpdateSlotSelect();
            }
        }
    }


    /*=========================
     * 次のオブジェクト
     *=========================*/

    void SelectNextObject()
    {
        if (objects.Length == 0)
        {
            return;
        }

        currentObjectIndex++;

        if (currentObjectIndex >= objects.Length)
        {
            currentObjectIndex = 0;
        }

        Debug.Log(
            "選択オブジェクト : " +
            objects[currentObjectIndex].objectName
        );
    }


    /*=========================
     * 前のオブジェクト
     *=========================*/

    void SelectPreviousObject()
    {
        if (objects.Length == 0)
        {
            return;
        }

        currentObjectIndex--;

        if (currentObjectIndex < 0)
        {
            currentObjectIndex = objects.Length - 1;
        }

        Debug.Log(
            "選択オブジェクト : " +
            objects[currentObjectIndex].objectName
        );
    }


    /*=========================
     * 配置場所選択
     *=========================*/

    void PlacementSelectUpdate()
    {
        /* 上 */
        if (InputManager.Instance.Up())
        {
            MoveUp();
        }

        /* 下 */
        if (InputManager.Instance.Down())
        {
            MoveDown();
        }

        /* 左 */
        if (InputManager.Instance.Left())
        {
            MoveLeft();
        }

        /* 右 */
        if (InputManager.Instance.Right())
        {
            MoveRight();
        }

        /* 決定 */
        if (InputManager.Instance.Submit())
        {
            PlaceObject();
        }

        /* 戻る */
        if (InputManager.Instance.Back())
        {
            currentMode = CustomizeMode.ObjectSelect;

            /* カーソルを非表示 */
            HideSlotSelect();
        }
    }


    /*=========================
     * 上移動
     *=========================*/

    void MoveUp()
    {
        if (currentSlot == null)
        {
            return;
        }

        if (currentSlot.up != null)
        {
            currentSlot = currentSlot.up;

            UpdateSlotSelect();
        }
    }


    /*=========================
     * 下移動
     *=========================*/

    void MoveDown()
    {
        if (currentSlot == null)
        {
            return;
        }

        if (currentSlot.down != null)
        {
            currentSlot = currentSlot.down;

            UpdateSlotSelect();
        }
    }


    /*=========================
     * 左移動
     *=========================*/

    void MoveLeft()
    {
        if (currentSlot == null)
        {
            return;
        }

        if (currentSlot.left != null)
        {
            currentSlot = currentSlot.left;

            UpdateSlotSelect();
        }
    }


    /*=========================
     * 右移動
     *=========================*/

    void MoveRight()
    {
        if (currentSlot == null)
        {
            return;
        }

        if (currentSlot.right != null)
        {
            currentSlot = currentSlot.right;

            UpdateSlotSelect();
        }
    }


    /*=========================
     * カーソル表示更新
     *=========================*/

    void UpdateSlotSelect()
    {
        /* 全て非選択 */
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].SetSelect(false);
            }
        }

        /* 現在位置を選択 */
        if (currentSlot != null)
        {
            currentSlot.SetSelect(true);
        }
    }


    /*=========================
     * カーソル非表示
     *=========================*/

    void HideSlotSelect()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].SetSelect(false);
            }
        }
    }


    /*=========================
     * オブジェクト配置
     *=========================*/

    void PlaceObject()
    {
        if (currentSlot == null)
        {
            return;
        }

        StageObjectData data = objects[currentObjectIndex];

        /* 個数確認 */
        if (data.remainCount <= 0)
        {
            return;
        }

        /* 配置 */
        currentSlot.PlaceObject(data.prefab);

        /* 個数を減らす */
        data.remainCount--;

        Debug.Log(
            data.objectName +
            " を配置 : 残り " +
            data.remainCount
        );

        /* オブジェクト選択へ戻る */
        currentMode = CustomizeMode.ObjectSelect;

        /* 配置カーソルを非表示 */
        HideSlotSelect();
    }
}