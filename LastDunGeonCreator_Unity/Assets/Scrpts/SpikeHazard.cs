
using UnityEngine;

public class SpikeHazard : MonoBehaviour
{
    [Header("ダメージ間隔")]
    [SerializeField]
    private float damageInterval = 1.0f;

    private float nextDamageTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryDamage(other);
    }

    private void TryDamage(Collider2D other)
    {
        PlayerState state =
            other.GetComponentInParent<PlayerState>();

        if (state == null)
            return;

        if (Time.time < nextDamageTime)
            return;

        state.TakeDamage();

        nextDamageTime = Time.time + damageInterval;

        Debug.Log("トゲダメージ！ HP: " + state.GetHP());
    }
}
