using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour, IUIActive
{
    [Header("코인")]
    [SerializeField] private TextMeshProUGUI _totalCoin;

    public void UpdateCoinText(int value)
    {
        _totalCoin.text = value.ToString();
    }

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
        }
    }

    private void SetGameMode()
    {
        gameObject.SetActive(false);
    }

    private void SetDefaultMode()
    {
        gameObject.SetActive(true);
    }
    #endregion
}
