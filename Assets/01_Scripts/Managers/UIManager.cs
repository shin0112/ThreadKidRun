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
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private GameOverUI _gameOverUI;
    private List<IUIActive> _uiActives = new();

    [Header("텍스트")]
    [SerializeField] private GameObject _startText;

    // 카메라
    private Camera _mainCamera;

    private void Reset()
    {
        _settingUI = transform.FindChild<SettingUI>("SettingWindow");
        _pauseUI = transform.FindChild<PauseUI>("PauseWindow");
        _gameOverUI = transform.FindChild<GameOverUI>("GameOverWindow");
        _totalCoinUI = transform.FindChild<CoinUI>("TotalCoin");
        _scoreUI = transform.FindChild<ScoreUI>("Score");
        _shopUI = transform.FindChild<ShopUI>("Shop");

        _startText = transform.FindChild<Transform>("StartText").gameObject;
    }

    private void Awake()
    {
        _instance = this;

        Init();
    }

    private void Start()
    {
        // camera
        _mainCamera = Camera.main;

        // event
        GameManager.Instance.OnScoreChanged += ScoreEvents;

        SetDefaultMode();
    }

    private void Update()
    {
        if (_startText.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            SetGameMode();
        }
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
        _scoreUI.UpdateCurrentScore(value);
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

        // 켜질 경우 점수 업데이트
        if (_pauseUI.gameObject.activeSelf)
        {
            _pauseUI.UpdateCurrentScoreText(_scoreUI.CurScore);
        }
    }

    public void ShowGameOverWindow()
    {
        _gameOverUI.gameObject.SetActive(true);
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
        _mainCamera.gameObject.SetActive(true);
    }

    public void SetShopMode()
    {
        _uiActives.ForEach(ui => ui.SetShopMode());
        _startText.SetActive(false);
        _mainCamera.gameObject.SetActive(false);
    }
    #endregion
}
