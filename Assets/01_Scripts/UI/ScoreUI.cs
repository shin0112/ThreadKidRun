using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour, IUIActive
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _curScoreText;

    [Header("텍스트 컨테이너")]
    [SerializeField] private GameObject _curScoreContainer;

    private int _bestScore = 0;
    [SerializeField] private int _curScore = 0;
    public int CurScore => _curScore;

    public void UpdateCurrentScore(int value)
    {
        _curScore = value;
        _curScoreText.text = _curScore.ToString();

        if (_curScore > _bestScore) UpdateBestScore(_curScore);
    }

    private void UpdateBestScore(int value)
    {
        _bestScore = value;
        _bestScoreText.text = _bestScore.ToString();
    }

    #region 인터페이스 구현
    public void SetDefaultMode()
    {
        _curScoreContainer.SetActive(false);
    }

    public void SetGameMode()
    {
        _curScoreContainer.SetActive(true);
    }
    #endregion
}
