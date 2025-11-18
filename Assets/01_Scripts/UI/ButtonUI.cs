using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IUIActive
{
    [Header("아이콘 버튼")]
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;

    private void Start()
    {
        UIManager instance = UIManager.Instance;

        _settingButton.onClick.AddListener(instance.ToggleSettingUI);
        _storeButton.onClick.AddListener(instance.SetShopMode);
        _pauseButton.onClick.AddListener(instance.TogglePauseUI);
        _homeButton.onClick.AddListener(instance.SetDefaultMode);
    }

    private void OnDestroy()
    {
        _settingButton.onClick.RemoveAllListeners();
        _storeButton.onClick.RemoveAllListeners();
        _pauseButton.onClick.RemoveAllListeners();
        _homeButton.onClick.RemoveAllListeners();
    }

    #region 인터페이스 구현
    public void SetGameMode()
    {
        _storeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void SetDefaultMode()
    {
        _storeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _homeButton.gameObject.SetActive(false);
    }

    public void SetShopMode()
    {
        _storeButton.gameObject.SetActive(false);
        _homeButton.gameObject.SetActive(true);
    }
    #endregion
}
