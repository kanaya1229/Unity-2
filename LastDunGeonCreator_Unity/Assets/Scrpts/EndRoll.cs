
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRoll : MonoBehaviour
{
    [Header("エンドロール")]
    [SerializeField] private RectTransform rollText;

    [Header("スクロール速度")]
    [SerializeField] private float rollSpeed = 70.0f;

    [Header("終了後の待ち時間")]
    [SerializeField] private float endWaitTime = 3.0f;

    [Header("エンドロールBGM")]
    [SerializeField] private AudioSource bgmAudioSource;

    [Header("再生するBGM")]
    [SerializeField] private AudioClip endRollBGM;

    [Header("タイトルScene")]
    [SerializeField] private string titleSceneName = "StartScene";

    private bool isFinished = false;
    private bool isTransitioning = false;
    private float waitTimer = 0.0f;

    private void Start()
    {
        // エンドロールBGMを再生
        if (bgmAudioSource != null && endRollBGM != null)
        {
            bgmAudioSource.Stop();
            bgmAudioSource.clip = endRollBGM;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }

    private void Update()
    {
        if (isTransitioning)
            return;

        // エンドロール終了後
        if (isFinished)
        {
            waitTimer += Time.unscaledDeltaTime;

            if (waitTimer >= endWaitTime)
            {
                EndRollFinish();
            }

            return;
        }

        if (rollText == null)
            return;

        // エンドロールを上方向へ移動
        rollText.anchoredPosition +=
            Vector2.up * rollSpeed * Time.unscaledDeltaTime;

        // 画面外まで移動したら終了
        if (rollText.anchoredPosition.y >= 3100.0f)
        {
            isFinished = true;
        }
    }

    private void EndRollFinish()
    {
        if (isTransitioning)
            return;

        isTransitioning = true;

        // BGMを停止
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }

        // タイトルに戻る
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleSceneName);
    }

    // エンドロールをスキップ
    public void SkipEndRoll()
    {
        EndRollFinish();
    }
}
