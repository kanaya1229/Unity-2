
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageGoal : MonoBehaviour
{
    [Header("Ÿ‚ÉˆÚ“®‚·‚éScene–¼")]
    [SerializeField] private string nextSceneName;

    private bool isGoal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGoal)
            return;

        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player == null)
            return;

        isGoal = true;

        GoToNextScene();
    }

    private void GoToNextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Ÿ‚ÌScene–¼‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
            isGoal = false;
            return;
        }

        Debug.Log("ƒS[ƒ‹“’BI Ÿ‚ÌScene‚ÖˆÚ“®");

        SceneManager.LoadScene(nextSceneName);
    }
}
