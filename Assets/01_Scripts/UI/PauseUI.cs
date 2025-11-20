using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI _score;

    [Header("버튼")]
    [SerializeField] private Button _continue;
    [SerializeField] private Button _exit;

    private void OnEnable()
    {
        _continue.onClick.AddListener(ContinueGame);
        _exit.onClick.AddListener(ExitGame);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        _continue.onClick.RemoveAllListeners();
        _exit.onClick.RemoveAllListeners();
        Time.timeScale = 1f;
    }

    public void UpdateCurrentScoreText(int value)
    {
        _score.text = value.ToString();
    }

    private void ContinueGame()
    {
        // todo: 게임 재시작
        gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        gameObject.SetActive(false);
        UIManager.Instance.GameReload();
    }
}
