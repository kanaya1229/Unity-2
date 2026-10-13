
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [Header("画面")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider volumeSlider;

    [Header("ポーズ項目（SETTINGS → TITLE → EXIT）")]
    [SerializeField] private RectTransform[] pauseItems;

    [Header("選択矢印")]
    [SerializeField] private RectTransform selectArrow;
    [SerializeField]
    private Vector2 arrowOffset =
        new Vector2(-35f, 0f);

    [Header("音量調整")]
    [SerializeField] private float volumeStep = 0.05f;

    [Header("各項目の矢印位置")]
    [SerializeField] private GameObject[] selectArrows;

    private int selectedIndex;
    private bool isSettingOpen;

    void Start()
    {
        IsPaused = false;
        isSettingOpen = false;
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        settingPanel.SetActive(false);

        if (AudioManager.Instance != null)
            volumeSlider.SetValueWithoutNotify(
                AudioManager.Instance.GetVolume());
    }

    void Update()
    {
        if (InputManager.Instance == null)
            return;

        if (InputManager.Instance.Pause())
        {
            if (IsPaused)
                ResumeGame();
            else
                PauseGame();

            return;
        }

        if (!IsPaused)
            return;

        if (isSettingOpen)
        {
            SettingInput();
            return;
        }

        if (InputManager.Instance.Up())
            MoveSelection(-1);
        else if (InputManager.Instance.Down())
            MoveSelection(1);

        if (InputManager.Instance.Submit())
            ExecuteSelection();
    }

    void MoveSelection(int direction)
    {
        if (pauseItems == null || pauseItems.Length == 0)
            return;

        selectedIndex =
            (selectedIndex + direction + pauseItems.Length)
            % pauseItems.Length;

        UpdateArrow();
    }

    void UpdateArrow()
    {
        for (int i = 0; i < selectArrows.Length; i++)
        {
            if (selectArrows[i] != null)
            {
                selectArrows[i].SetActive(i == selectedIndex);
            }
        }
    }

    void ExecuteSelection()
    {
        switch (selectedIndex)
        {
            case 0:
                OpenSetting();
                break;

            case 1:
                ReturnToTitle();
                break;

            case 2:
                ResumeGame();
                break;
        }
    }

    void SettingInput()
    {
        if (InputManager.Instance.Left())
        {
            volumeSlider.value =
                Mathf.Clamp01(volumeSlider.value - volumeStep);
            ChangeVolume();
        }
        else if (InputManager.Instance.Right())
        {
            volumeSlider.value =
                Mathf.Clamp01(volumeSlider.value + volumeStep);
            ChangeVolume();
        }

        if (InputManager.Instance.Back() ||
            InputManager.Instance.Submit())
        {
            CloseSetting();
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        isSettingOpen = false;
        selectedIndex = 0;

        pausePanel.SetActive(true);
        settingPanel.SetActive(false);

        Time.timeScale = 0f;
        UpdateArrow();
    }

    public void ResumeGame()
    {
        IsPaused = false;
        isSettingOpen = false;

        pausePanel.SetActive(false);
        settingPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OpenSetting()
    {
        isSettingOpen = true;

        pausePanel.SetActive(false);
        settingPanel.SetActive(true);

        if (AudioManager.Instance != null)
            volumeSlider.SetValueWithoutNotify(
                AudioManager.Instance.GetVolume());
    }

    public void CloseSetting()
    {
        isSettingOpen = false;

        settingPanel.SetActive(false);
        pausePanel.SetActive(true);

        UpdateArrow();
    }

    public void ChangeVolume()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetVolume(volumeSlider.value);
    }

    public void ReturnToTitle()
    {
        ResumeGame();
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        ResumeGame();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
