
using UnityEngine;

public class StageCameraController : MonoBehaviour
{
    [Header("実際に動かすカメラ")]
    [SerializeField] private Camera targetCamera;

    [Header("前半・後半のカメラ位置")]
    [SerializeField] private Transform frontPoint;
    [SerializeField] private Transform backPoint;

    [Header("前半・後半の境目")]
    [SerializeField] private Transform cameraSwitchPoint;

    private Transform player;
    private bool isPlaying = false;
    private bool isBack = false;

    private void Awake()
    {
        // カメラが未設定ならMainCameraタグから取得
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (targetCamera == null)
        {
            Debug.LogError(
                "Main Cameraを取得できません！"
            );
        }
    }

    private void Start()
    {
        MoveCamera(false);
    }

    private void LateUpdate()
    {
        if (!isPlaying || player == null)
            return;

        if (cameraSwitchPoint == null)
            return;

        // 勇者が境目を越えたか判定
        bool shouldBeBack =
            player.position.x >= cameraSwitchPoint.position.x;

        if (shouldBeBack != isBack)
        {
            MoveCamera(shouldBeBack);
        }
    }

    // カスタマイズ中のカメラ切り替え
    public void SetCustomizeSide(bool back)
    {
        if (isPlaying)
            return;

        MoveCamera(back);
    }

    // 勇者操作開始
    public void StartPlayerCamera(Transform playerTransform)
    {
        player = playerTransform;
        isPlaying = true;

        if (player != null && cameraSwitchPoint != null)
        {
            bool shouldBeBack =
                player.position.x >= cameraSwitchPoint.position.x;

            MoveCamera(shouldBeBack);
        }
    }

    private void MoveCamera(bool back)
    {
        if (targetCamera == null)
            return;

        Transform point = back ? backPoint : frontPoint;

        if (point == null)
        {
            Debug.LogError(
                "カメラ位置が未設定です！"
            );
            return;
        }

        isBack = back;

        // 実際のMain Cameraを移動
        targetCamera.transform.position = point.position;

        Debug.Log(
            "カメラ切り替え：" +
            (back ? "後半" : "前半") +
            " / 位置：" +
            targetCamera.transform.position
        );
    }
}
