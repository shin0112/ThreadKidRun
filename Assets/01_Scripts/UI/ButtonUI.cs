using GameName.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IUIActive
{
    [Header("아이콘 버튼")]
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _archievementButton;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;

    [Header("코인 테스트용")]
    [SerializeField] private Button _getCoinButton;
    [SerializeField] private Button _resetCoinButton;


    private void Start()
    {
        _settingButton.onClick.AddListener(OnClickSettingButton);
        _archievementButton.onClick.AddListener(OnClickArchievementButton);
        _storeButton.onClick.AddListener(OnClickStoreButton);
        _pauseButton.onClick.AddListener(OnClickPauseButton);
        _homeButton.onClick.AddListener(OnClickHomeButton);

        // 코인 테스트용
        _getCoinButton.onClick.AddListener(() => GameManager.Instance.EarnCoin(10000));
        _resetCoinButton.onClick.AddListener(GameManager.Instance.ResetCoin);
    }

    private void OnDestroy()
    {
        _settingButton.onClick.RemoveAllListeners();
        _archievementButton.onClick.RemoveAllListeners();
        _storeButton.onClick.RemoveAllListeners();
        _pauseButton.onClick.RemoveAllListeners();
        _homeButton.onClick.RemoveAllListeners();

        // 코인 테스트용
        _getCoinButton.onClick.RemoveAllListeners();
        _resetCoinButton.onClick.RemoveAllListeners();
    }

    #region 버튼 클릭 이벤트
    private void OnClickSettingButton()
    {
        UIManager.Instance.ToggleSettingUI();
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }

    private void OnClickArchievementButton()
    {
        UIManager.Instance.ToggleArchievementUI();
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }

    private void OnClickStoreButton()
    {
        UIManager.Instance.SetShopMode();
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }

    private void OnClickPauseButton()
    {
        UIManager.Instance.TogglePauseUI();
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }

    private void OnClickHomeButton()
    {
        UIManager.Instance.SetDefaultMode();
        AudioManager.Instance.PlaySFX("SFX_ButtonClick");
    }
    #endregion

    #region 인터페이스 구현
    public void SetMode(UIMode mode)
    {
        switch (mode)
        {
            case UIMode.Default:
                SetDefaultMode();
                break;
            case UIMode.Game:
                SetGameMode();
                break;
            case UIMode.Shop:
                SetShopMode();
                break;
        }
    }

    private void SetGameMode()
    {
        _storeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _archievementButton.gameObject.SetActive(false);
    }

    private void SetDefaultMode()
    {
        _storeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _homeButton.gameObject.SetActive(false);
        _archievementButton.gameObject.SetActive(true);
    }

    private void SetShopMode()
    {
        _storeButton.gameObject.SetActive(false);
        _homeButton.gameObject.SetActive(true);
        _archievementButton.gameObject.SetActive(false);
    }
    #endregion
}
