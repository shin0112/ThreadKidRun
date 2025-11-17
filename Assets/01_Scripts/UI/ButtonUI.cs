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
        _pauseButton.onClick.AddListener(instance.TogglePauseUI);
        // todo: store button - ui 만들고 연결
    }

    private void OnDestroy()
    {
        _settingButton.onClick.RemoveAllListeners();
        _storeButton.onClick.RemoveAllListeners();
        _pauseButton.onClick.RemoveAllListeners();
    }

    public void SetGameMode()
    {
        _storeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void SetDefaultMode()
    {
        _storeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }
}
