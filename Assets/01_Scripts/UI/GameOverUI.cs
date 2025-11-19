using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour, IUIActive
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI _score;

    [Header("버튼")]
    [SerializeField] private Button _retry;
    [SerializeField] private Button _exit;

    private void OnEnable()
    {
        _retry.onClick.AddListener(RetryGame);
        _exit.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _retry.onClick.RemoveAllListeners();
        _exit.onClick.RemoveAllListeners();
    }

    public void UpdateGameOverScore(int value)
    {
        _score.text = value.ToString();
    }

    private void RetryGame()
    {
        // todo: 게임 재시작
        UIManager.Instance.SetGameMode();
    }

    private void ExitGame()
    {
        // todo: 게임 그만하기
        UIManager.Instance.SetDefaultMode();
    }

    #region 인터페이스 구현
    public void SetGameMode()
    {
        gameObject.SetActive(false);
    }

    public void SetDefaultMode()
    {
        gameObject.SetActive(false);
    }

    public void SetShopMode()
    {
    }
    #endregion
}
