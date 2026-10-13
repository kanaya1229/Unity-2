
using UnityEngine;

public class StageCustomizeManager : MonoBehaviour
{
    [Header("配置場所（左から順番）")]
    [SerializeField] private StagePlacementSlot[] slots;

    [Header("配置オブジェクト")]
    [SerializeField] private StageObjectData[] objects;

    [Header("配置数")]
    [SerializeField] private int maxPlacementCount = 4;

    [Header("オブジェクト画像")]
    [SerializeField] private StageObjectUI objectUI;

    [Header("数字画像")]
    [SerializeField] private StageNumberUI totalRemainUI;
    [SerializeField] private StageNumberUI objectRemainUI;

    [Header("エリア選択矢印")]
    [SerializeField] private GameObject objectSelectArrow;
    [SerializeField] private GameObject startSelectArrow;

    [Header("カスタマイズ専用UI")]
    [SerializeField] private GameObject customizeUIRoot;

    [Header("勇者")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("ゲーム中のHP・状態UI")]
    [SerializeField] private PlayerGameUI playerGameUI;

    [Header("ステージ3 カメラ（他ステージは未設定でOK）")]
    [SerializeField] private StageCameraController stageCamera;


    [Header("後半に属する配置枠")]
    [SerializeField] private StagePlacementSlot[] backSlots;


    [Header("BGM（ステージごとに設定）")]
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioClip customizeBGM;
    [SerializeField] private AudioClip stageBGM;

    private bool isPlaying = false;

    private enum SelectArea
    {
        Start,
        Placement,
        Object
    }

    private SelectArea currentArea = SelectArea.Object;

    private int currentObjectIndex = 0;
    private int currentSlotIndex = 0;
    private int placedCount = 0;

    private void Start()
    {
        // カスタマイズ開始時のBGM
        PlayBGM(customizeBGM);

        // 配置可能数の初期化
        if (objects != null)
        {
            foreach (StageObjectData data in objects)
            {
                if (data != null)
                    data.remainCount = data.maxCount;
            }
        }

        // すでに配置されている数を取得
        placedCount = 0;

        if (slots != null)
        {
            foreach (StagePlacementSlot slot in slots)
            {
                if (slot != null && slot.IsPlaced())
                {
                    placedCount++;

                    int index = slot.placedObjectIndex;

                    if (objects != null &&
                        index >= 0 &&
                        index < objects.Length &&
                        objects[index] != null)
                    {
                        objects[index].remainCount =
                            Mathf.Max(
                                0,
                                objects[index].remainCount - 1
                            );
                    }
                }
            }
        }

        // ゲーム中UIは開始前に非表示
        if (playerGameUI != null)
            playerGameUI.gameObject.SetActive(false);

        UpdateObjectUI();
        UpdateNumberUI();
        UpdateSelection();

        // 初期カメラは前半
        if (stageCamera != null)
            stageCamera.SetCustomizeSide(false);
    }

    private void Update()
    {
        if (isPlaying ||
            PauseManager.IsPaused ||
            InputManager.Instance == null)
            return;

        if (InputManager.Instance.Up())
        {
            ChangeArea(-1);
        }
        else if (InputManager.Instance.Down())
        {
            ChangeArea(1);
        }

        if (InputManager.Instance.Left())
        {
            MoveHorizontal(-1);
        }
        else if (InputManager.Instance.Right())
        {
            MoveHorizontal(1);
        }

        if (InputManager.Instance.Submit())
        {
            Submit();
        }

        if (InputManager.Instance.Back())
        {
            currentArea = SelectArea.Object;
            UpdateSelection();
        }
    }

    private void ChangeArea(int direction)
    {
        const int areaCount = 3;

        int next =
            ((int)currentArea + direction + areaCount)
            % areaCount;

        currentArea = (SelectArea)next;

        UpdateSelection();
    }

    private void MoveHorizontal(int direction)
    {
        if (currentArea == SelectArea.Object)
        {
            if (objects == null || objects.Length == 0)
                return;

            currentObjectIndex =
                (currentObjectIndex + direction + objects.Length)
                % objects.Length;

            UpdateObjectUI();
            UpdateNumberUI();
        }
        else if (currentArea == SelectArea.Placement)
        {
            if (slots == null || slots.Length == 0)
                return;

            currentSlotIndex =
                (currentSlotIndex + direction + slots.Length)
                % slots.Length;

            UpdateSelection();
        }
    }

    private void Submit()
    {
        switch (currentArea)
        {
            case SelectArea.Start:
                StartGame();
                break;

            case SelectArea.Placement:
                PlaceObject();
                break;

            case SelectArea.Object:
                currentArea = SelectArea.Placement;
                UpdateSelection();
                break;
        }
    }

    private void UpdateSelection()
    {
        if (objectSelectArrow != null)
        {
            objectSelectArrow.SetActive(
                currentArea == SelectArea.Object
            );
        }

        if (startSelectArrow != null)
        {
            startSelectArrow.SetActive(
                currentArea == SelectArea.Start
            );
        }

        if (slots != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != null)
                {
                    slots[i].SetSelect(
                        currentArea == SelectArea.Placement &&
                        i == currentSlotIndex
                    );
                }
            }
        }



        if (stageCamera != null)
        {
            if (currentArea == SelectArea.Placement &&
                slots != null &&
                currentSlotIndex >= 0 &&
                currentSlotIndex < slots.Length)
            {
                StagePlacementSlot selectedSlot = slots[currentSlotIndex];

                bool isBack = false;

                if (backSlots != null)
                {
                    foreach (StagePlacementSlot slot in backSlots)
                    {
                        if (slot == selectedSlot)
                        {
                            isBack = true;
                            break;
                        }
                    }
                }

                stageCamera.SetCustomizeSide(isBack);
            }
            else if (currentArea == SelectArea.Start)
            {
                stageCamera.SetCustomizeSide(false);
            }
        }


    }

    private void UpdateObjectUI()
    {
        if (objectUI == null ||
            objects == null ||
            objects.Length == 0)
            return;

        if (objects[currentObjectIndex] == null)
            return;

        objectUI.SetObjectSprite(
            objects[currentObjectIndex].iconSprite
        );
    }

    private void UpdateNumberUI()
    {
        if (totalRemainUI != null)
        {
            totalRemainUI.SetNumber(
                Mathf.Max(0, maxPlacementCount - placedCount)
            );
        }

        if (objectRemainUI != null &&
            objects != null &&
            objects.Length > 0 &&
            objects[currentObjectIndex] != null)
        {
            objectRemainUI.SetNumber(
                objects[currentObjectIndex].remainCount
            );
        }
    }

    private void PlaceObject()
    {
        if (slots == null ||
            currentSlotIndex < 0 ||
            currentSlotIndex >= slots.Length)
            return;

        if (objects == null ||
            currentObjectIndex < 0 ||
            currentObjectIndex >= objects.Length)
            return;

        StagePlacementSlot slot = slots[currentSlotIndex];
        StageObjectData newData = objects[currentObjectIndex];

        if (slot == null ||
            newData == null ||
            newData.prefab == null)
            return;

        bool isReplacing = slot.IsPlaced();
        int oldIndex = slot.placedObjectIndex;

        // 同じ種類なら配置し直さない
        if (isReplacing && oldIndex == currentObjectIndex)
            return;

        // 残り数を確認
        if (newData.remainCount <= 0)
            return;

        // 新規配置の場合は全体上限を確認
        if (!isReplacing && placedCount >= maxPlacementCount)
            return;

        // 置き換え前のオブジェクト数を戻す
        if (isReplacing &&
            oldIndex >= 0 &&
            oldIndex < objects.Length &&
            objects[oldIndex] != null)
        {
            objects[oldIndex].remainCount++;
        }

        // オブジェクト配置
        slot.PlaceObject(newData.prefab);
        slot.placedObjectIndex = currentObjectIndex;

        newData.remainCount--;

        if (!isReplacing)
            placedCount++;

        UpdateNumberUI();

        Debug.Log(
            newData.objectName +
            (isReplacing ? " に置き換えました" : " を配置しました")
        );
    }

    private void PlayBGM(AudioClip clip)
    {
        if (bgmAudioSource == null)
        {
            Debug.LogWarning("BGM用AudioSourceが未設定です");
            return;
        }

        bgmAudioSource.Stop();
        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;

        if (clip != null)
            bgmAudioSource.Play();
        else
            Debug.LogWarning("BGMのAudioClipが未設定です");
    }

    private void StartGame()
    {
        if (isPlaying)
            return;

        if (playerPrefab == null || playerSpawnPoint == null)
        {
            Debug.LogError(
                "勇者Prefabか召喚位置が未設定です"
            );
            return;
        }

        // STARTを押したらステージ専用BGMに切り替え
        PlayBGM(stageBGM);

        isPlaying = true;

        // 配置枠の選択矢印を非表示
        if (slots != null)
        {
            foreach (StagePlacementSlot slot in slots)
            {
                if (slot != null)
                    slot.SetSelect(false);
            }
        }

        // カスタマイズUIを非表示
        if (customizeUIRoot != null)
            customizeUIRoot.SetActive(false);

        // 勇者を生成
        GameObject player = Instantiate(
            playerPrefab,
            playerSpawnPoint.position,
            playerSpawnPoint.rotation
        );

        // HP・状態異常UIを開始
        if (playerGameUI != null)
        {
            playerGameUI.SetPlayer(player);
        }
        else
        {
            Debug.LogWarning(
                "PlayerGameUIが未設定です"
            );
        }

        // カメラを勇者の位置で切り替え
        if (stageCamera != null)
        {
            stageCamera.StartPlayerCamera(
                player.transform
            );
        }

        Debug.Log("勇者操作開始！");
    }
}
