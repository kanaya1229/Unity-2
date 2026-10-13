
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingSlideShow : MonoBehaviour
{
    [Header("共通の1枚目")]
    [SerializeField] private Sprite firstImage;

    [Header("通常エンド（2～4枚目）")]
    [SerializeField] private Sprite[] normalImages;

    [Header("鍵3個エンド（2～4枚目）")]
    [SerializeField] private Sprite[] keyImages;

    [Header("表示用Image")]
    [SerializeField] private Image displayImage;

    [Header("暗転用Image")]
    [SerializeField] private Image fadeImage;

    [Header("時間設定")]
    [SerializeField] private float fadeInTime = 1.5f;
    [SerializeField] private float displayTime = 3f;
    [SerializeField] private float fadeOutTime = 1.5f;

    [Header("エンドロールScene")]
    [SerializeField] private string creditSceneName = "CreditScene";

    private void Start()
    {
        if (displayImage == null || fadeImage == null ||
            firstImage == null)
        {
            Debug.LogError("EndingSlideShowの設定が不足しています");
            return;
        }

        SetFadeAlpha(1f);
        StartCoroutine(PlayEnding());
    }

    private IEnumerator PlayEnding()
    {
        // GameManagerから鍵の数を取得
        int keyCount = 0;

        if (GameManager.Instance != null)
        {
            keyCount = GameManager.Instance.GetKeyCount();
        }

        // 鍵3個以上なら特殊エンド
        bool isKeyEnding = keyCount >= 3;

        Sprite[] selectedImages =
            isKeyEnding ? keyImages : normalImages;

        Debug.Log(
            "エンディング開始：鍵 " + keyCount + " 個"
        );

        // 1枚目は共通
        yield return StartCoroutine(ShowImage(firstImage));

        // 2～4枚目は分岐
        foreach (Sprite sprite in selectedImages)
        {
            if (sprite == null)
                continue;

            yield return StartCoroutine(ShowImage(sprite));
        }

        // エンドロールへ
        Time.timeScale = 1f;
        SceneManager.LoadScene(creditSceneName);
    }

    private IEnumerator ShowImage(Sprite sprite)
    {
        displayImage.sprite = sprite;

        // フェードイン
        yield return StartCoroutine(
            Fade(1f, 0f, fadeInTime)
        );

        // 表示
        yield return new WaitForSecondsRealtime(displayTime);

        // フェードアウト
        yield return StartCoroutine(
            Fade(0f, 1f, fadeOutTime)
        );
    }

    private IEnumerator Fade(
        float startAlpha,
        float endAlpha,
        float duration)
    {
        if (duration <= 0f)
        {
            SetFadeAlpha(endAlpha);
            yield break;
        }

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            float rate = Mathf.Clamp01(timer / duration);

            SetFadeAlpha(
                Mathf.Lerp(startAlpha, endAlpha, rate)
            );

            yield return null;
        }

        SetFadeAlpha(endAlpha);
    }

    private void SetFadeAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
