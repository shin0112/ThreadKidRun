// ============================================
using GameName.Managers;  //오디오연결
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ============================================

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("공통")]
    [SerializeField] private CoinUI _totalCoinUI;
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private TutorialUI _tutorialUI;
    public CoinUI CoinUI => _totalCoinUI;
    public ShopUI ShopUI => _shopUI;
    public TutorialUI TutorialUI => _tutorialUI;

    [Header("팝업창")]
    [SerializeField] private SettingUI _settingUI;
    [SerializeField] private PauseUI _pauseUI;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private GameObject _archievementUI;

    [Header("텍스트")]
    [SerializeField] private GameObject _startText;

    // 씬 로드 모드
    private UIMode _curLoadMode = UIMode.Default;
    public UIMode CurLoadMode
    {
        get { return _curLoadMode; }
        set { _curLoadMode = value; }
    }

    // 카메라
    private Camera _mainCamera;
    public Camera Camera
    {
        get { return _mainCamera; }
        set { _mainCamera = value; }
    }

    private void Reset()
    {
        _settingUI = transform.FindChild<SettingUI>("SettingWindow");
        _pauseUI = transform.FindChild<PauseUI>("PauseWindow");
        _gameOverUI = transform.FindChild<GameOverUI>("GameOverWindow");
        _totalCoinUI = transform.FindChild<CoinUI>("TotalCoin");
        _scoreUI = transform.FindChild<ScoreUI>("Score");
        _shopUI = transform.FindChild<ShopUI>("Shop");
        _tutorialUI = transform.FindChild<TutorialUI>("Tutorial");
        _archievementUI = transform.FindChild<Transform>("AchievementCanvas").gameObject;

        _startText = transform.FindChild<Transform>("StartText").gameObject;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        Init();

        // camera
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        // event
        GameManager.Instance.OnScoreChanged += ScoreEvents;

        SetDefaultMode();

        // ============================================
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM("BGM_MainTheme");
        }
        // ============================================
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

    /// <summary>
    /// 씬 초기화
    /// </summary>
    private List<IUIActive> _uiActives = new();
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

    public void SetSceneLoadMode()
    {
        switch (_curLoadMode)
        {
            case UIMode.Default:
                SetDefaultMode();
                break;
            case UIMode.Shop:
                SetShopMode();
                break;
        }
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

    public void ToggleArchievementUI()
    {
        _archievementUI.gameObject.Toggle();
    }

    public void ShowGameOverWindow()
    {
        _gameOverUI.gameObject.SetActive(true);
    }
    #endregion  

    #region 게임 오브젝트 On/Off
    public void SetGameMode()
    {
        _uiActives.ForEach(ui => ui.SetMode(UIMode.Game));
        _startText.SetActive(false);
    }

    public void SetDefaultMode()
    {
        _uiActives.ForEach(ui => ui.SetMode(UIMode.Default));
        _startText.SetActive(true);
        _mainCamera.gameObject.SetActive(true);
        AudioManager.Instance?.PlayBGM("BGM_MainTheme");
    }

    public void SetShopMode()
    {
        _uiActives.ForEach(ui => ui.SetMode(UIMode.Shop));
        _startText.SetActive(false);
        _mainCamera.gameObject.SetActive(false);
        AudioManager.Instance?.PlayBGM("BGM_Shop");
    }
    #endregion
}
