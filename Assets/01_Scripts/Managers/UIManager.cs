using System;
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
    [SerializeField] private CoinUI _coinUI;

    // 이벤트
    public Action<int> OnCoinChanged;

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

    #region 아이콘 On/Off
    public void SetGameMode()
    {
        _storeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void SetMainMode()
    {
        _storeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }
    #endregion
}
