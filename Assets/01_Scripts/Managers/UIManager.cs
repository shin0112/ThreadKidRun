using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("아이콘 버튼")]
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;

    [Header("UI")]
    [SerializeField] private SettingUI _settingUI;
    [SerializeField] private CoinUI _totalCoinUI;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private PauseUI _pauseUI;

    // 이벤트
    public Action<int> OnCoinChanged;
    [Header("텍스트")]
    [SerializeField] private GameObject _startText;
    [SerializeField] private GameObject _currentScore;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _settingUI.gameObject.SetActive(false);
        SetMainMode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetGameMode();
        }
    }

    private void OnEnable()
    {
        // button
        _settingButton.onClick.AddListener(ToggleSettingUI);
        // todo: store button - ui 만들고 연결
        // todo: pause button - 게임 멈춤 로직 연결

        // event
        OnCoinChanged += _coinUI.UpdateCoinText;
    }

    private void OnDisable()
    {
        // button
        _settingButton.onClick.RemoveAllListeners();
        _storeButton.onClick.RemoveAllListeners();

        // event
        OnCoinChanged -= _coinUI.UpdateCoinText;
    }

    private void ToggleSettingUI()
    {
        _settingUI.gameObject.Toggle();
    }

    #region 게임 오브젝트 On/Off
    public void SetGameMode()
    {
        // 버튼
        _storeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);

        // 아이콘
        _totalCoinUI.gameObject.SetActive(false);

        // 텍스트
        _startText.SetActive(false);
        _currentScore.SetActive(true);
    }

    public void SetMainMode()
    {
        // 버튼
        _storeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);

        // 아이콘
        _totalCoinUI.gameObject.SetActive(true);

        // 텍스트
        _startText.SetActive(true);
        _currentScore.SetActive(false);
    }
    #endregion
}
