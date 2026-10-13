
using UnityEngine;

public class StageCameraController : MonoBehaviour
{
    [Header("前半・後半のカメラ位置")]
    [SerializeField] private Transform frontPoint;
    [SerializeField] private Transform backPoint;

    [Header("切り替え時の余白")]
    [SerializeField] private float screenMargin = 0.05f;

    private Camera stageCamera;
    private Transform player;

    private bool isPlaying = false;
    private bool isBack = false;

    private void Awake()
    {
        stageCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        SetCameraPosition(false);
    }

    // カスタマイズ中：選択枠が画面外なら切り替え
    public void ShowCustomizeTarget(Vector3 targetPosition)
    {
        if (isPlaying || stageCamera == null)
            return;

        if (!IsVisible(targetPosition))
        {
            SetCameraPosition(!isBack);
        }
    }

    // START選択時など
    public void SetCustomizeSide(bool back)
    {
        if (isPlaying)
            return;

        SetCameraPosition(back);
    }

    // プレイ開始
    public void StartPlayerCamera(Transform playerTransform)
    {
        player = playerTransform;
        isPlaying = true;

        if (player != null && !IsVisible(player.position))
        {
            SetCameraPosition(false);
        }
    }

    private void LateUpdate()
    {
        if (!isPlaying || player == null)
            return;

        // 勇者が現在のカメラ範囲から出たら切り替え
        if (!IsVisible(player.position))
        {
            SetCameraPosition(!isBack);
        }
    }

    private bool IsVisible(Vector3 worldPosition)
    {
        Vector3 viewport =
            stageCamera.WorldToViewportPoint(worldPosition);

        return viewport.z > 0f &&
               viewport.x >= screenMargin &&
               viewport.x <= 1f - screenMargin &&
               viewport.y >= 0f &&
               viewport.y <= 1f;
    }

    private void SetCameraPosition(bool back)
    {
        isBack = back;

        Transform target = isBack ? backPoint : frontPoint;

        if (target == null)
        {
            Debug.LogWarning(
                "カメラ位置が設定されていません"
            );
            return;
        }

        transform.position = target.position;
    }
}
