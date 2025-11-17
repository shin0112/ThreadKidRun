using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [Header("코인")]
    [SerializeField] private TextMeshProUGUI _totalCoin;

    public void UpdateCoinText(int value)
    {
        _totalCoin.text = value.ToString();
    }
}
