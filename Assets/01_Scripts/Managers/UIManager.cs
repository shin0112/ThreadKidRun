using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("UI")]
    [SerializeField] private SettingUI _settingUI;
    [SerializeField] private CoinUI _totalCoinUI;
    [SerializeField] private ScoreUI _scoreUI;
    private List<IUIActive> _uiActives = new();

    [Header("텍스트")]
    [SerializeField] private GameObject _startText;

    private void Awake()
    {
        _instance = this;

        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetGameMode();
        }
    }

    private void Start()
    {
        // event
        GameManager.Instance.OnScoreChanged += _totalCoinUI.UpdateCoinText;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnScoreChanged -= _totalCoinUI.UpdateCoinText;
    }

    private void Init()
    {
        _uiActives.Clear();
        _uiActives = GetComponentsInChildren<IUIActive>().ToList();
    }

    public void ToggleSettingUI()
    {
        _settingUI.gameObject.Toggle();
    }

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
    }
    #endregion
}
