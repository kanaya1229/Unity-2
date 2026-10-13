
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private string endingSceneName = "EndRoll";

    private bool cleared;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cleared ||
            other.GetComponentInParent<HeroEffectController>() == null)
        {
            return;
        }

        cleared = true;

        Debug.Log("ゴール到達。キー所持数: " +
                  other.GetComponentInParent<HeroEffectController>().KeyCount);

        SceneManager.LoadScene(endingSceneName);
    }
}