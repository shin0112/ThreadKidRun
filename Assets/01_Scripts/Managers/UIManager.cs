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

    [Header("UI")]
    [SerializeField] private GameObject _settingUI;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _settingUI.SetActive(false);
        SetMainMode();
    }

    private void OnEnable()
    {
        _settingButton.onClick.AddListener(ToggleSettingUI);
    }

    private void OnDisable()
    {
        _settingButton.onClick.RemoveAllListeners();
        _storeButton.onClick.RemoveAllListeners();
    }

    private void ToggleSettingUI()
    {
        _settingUI.Toggle();
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
