
using UnityEngine;

public class PlayerStatusEffectUI : MonoBehaviour
{
    [SerializeField] private GameObject fireIcon;
    [SerializeField] private GameObject lightIcon;

    private PlayerState playerState;
    private PlayerController playerController;

    public void SetPlayer(GameObject player)
    {
        playerState = player.GetComponent<PlayerState>();
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (fireIcon != null)
            fireIcon.SetActive(
                playerState != null && playerState.IsBurning()
            );

        if (lightIcon != null)
            lightIcon.SetActive(
                playerController != null && playerController.IsLight()
            );
    }
}
