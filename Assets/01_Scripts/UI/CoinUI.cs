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
    public void SetGameMode()
    {
        gameObject.SetActive(false);
    }

    public void SetDefaultMode()
    {
        gameObject.SetActive(true);
    }

    public void SetShopMode()
    {
        gameObject.SetActive(true);
    }
    #endregion
}
