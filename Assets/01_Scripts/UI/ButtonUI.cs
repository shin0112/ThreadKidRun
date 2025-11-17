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
        _settingButton.onClick.AddListener(UIManager.Instance.ToggleSettingUI);
        // todo: store button - ui 만들고 연결
        // todo: pause button - 게임 멈춤 로직 연결
    }

    private void OnDestroy()
    {
        _settingButton.onClick.RemoveAllListeners();
        _storeButton.onClick.RemoveAllListeners();
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
