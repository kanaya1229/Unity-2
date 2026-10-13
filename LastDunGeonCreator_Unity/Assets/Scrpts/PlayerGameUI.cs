
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameUI : MonoBehaviour
{
    [Header("HPバー")]
    [SerializeField] private Image hpBack;
    [SerializeField] private Image hpFill;

    [Header("状態アイコン")]
    [SerializeField] private GameObject fireIcon;
    [SerializeField] private GameObject lightIcon;

    private PlayerState playerState;
    private PlayerController playerController;

    private void Awake()
    {
        if (fireIcon != null)
            fireIcon.SetActive(false);

        if (lightIcon != null)
            lightIcon.SetActive(false);
    }

    public void SetPlayer(GameObject player)
    {
        if (player == null)
        {
            Debug.LogError("UI: Playerがnull");
            return;
        }

        playerState =
            player.GetComponentInChildren<PlayerState>();

        playerController =
            player.GetComponentInChildren<PlayerController>();

        if (playerState == null)
            Debug.LogError("UI: PlayerStateが見つからない");

        if (playerController == null)
            Debug.LogError("UI: PlayerControllerが見つからない");

        gameObject.SetActive(true);

        Debug.Log("UI: プレイヤー登録完了");

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerState == null)
            return;

        float hpRate = Mathf.Clamp01(
            (float)playerState.GetHP() /
            Mathf.Max(1, playerState.GetMaxHP())
        );

        if (hpFill != null)
            hpFill.fillAmount = hpRate;

        if (hpBack != null)
            hpBack.fillAmount = 1f;

        if (fireIcon != null)
            fireIcon.SetActive(playerState.IsBurning());

        if (lightIcon != null)
        {
            lightIcon.SetActive(
                playerController != null &&
                playerController.IsLight()
            );
        }
    }
}
