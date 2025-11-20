// ============================================
using GameName.Managers;  //오디오연결
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
// ============================================

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
    [SerializeField] private GameObject _archievementUI;
    private List<IUIActive> _uiActives = new();

    public CoinUI CoinUI => _totalCoinUI;
    public ShopUI ShopUI => _shopUI;

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
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        Init();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // camera
        _mainCamera = Camera.main;

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
    private void Init()
    {
        _uiActives.Clear();
        _uiActives = GetComponentsInChildren<IUIActive>().ToList();
    }

    public void GameReload()
    {
        SceneManager.LoadScene(SceneType.GameScene.ToString());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SetDefaultMode();
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
        _uiActives.ForEach(ui => ui.SetGameMode());
        _startText.SetActive(false);
    }

    public void SetDefaultMode()
    {
        _uiActives.ForEach(ui => ui.SetDefaultMode());
        _startText.SetActive(true);
        _mainCamera.gameObject.SetActive(true);
        // ============================================
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM("BGM_MainTheme");
        }
        // ============================================
    }

    public void SetShopMode()
    {
        _uiActives.ForEach(ui => ui.SetShopMode());
        _startText.SetActive(false);
        _mainCamera.gameObject.SetActive(false);

        // ============================================
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM("BGM_Shop");
        }
        // ============================================
    }
    #endregion
}
