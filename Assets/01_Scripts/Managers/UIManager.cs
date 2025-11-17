using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("UI")]
    [SerializeField] private SettingUI _settingUI;
    [SerializeField] private PauseUI _pauseUI;
    [SerializeField] private CoinUI _totalCoinUI;
    [SerializeField] private ScoreUI _scoreUI;
    private List<IUIActive> _uiActives = new();

    [Header("텍스트")]
    [SerializeField] private GameObject _startText;

    private void Awake()
    {
        _instance = this;

        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetGameMode();
        }
    }

    private void Start()
    {
        // event
        GameManager.Instance.OnScoreChanged += ScoreEvents;

        SetDefaultMode();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnScoreChanged -= ScoreEvents;
    }

    private void Init()
    {
        _uiActives.Clear();
        _uiActives = GetComponentsInChildren<IUIActive>().ToList();
    }

    /// <summary>
    /// 점수 관련 이벤트
    /// </summary>
    /// <param name="value"></param>
    private void ScoreEvents(int value)
    {
        _scoreUI.UpdataeCurrentScore(value);
    }

    #region Window On/Off
    /// <summary>
    /// 설정창 여닫기
    /// </summary>
    public void ToggleSettingUI()
    {
        _settingUI.gameObject.Toggle();
    }

    /// <summary>
    /// 일시정지창 여닫기
    /// </summary>
    public void TogglePauseUI()
    {
        _pauseUI.gameObject.Toggle();
    }
    #endregion  

    #region 게임 오브젝트 On/Off
    public void SetGameMode()
    {
        _uiActives.ForEach(ui => ui.SetGameMode());
        _startText.SetActive(false);
    }

    public void SetDefaultMode()
    {
        _uiActives.ForEach(ui => ui.SetDefaultMode());
        _startText.SetActive(true);
    }
    #endregion
}
