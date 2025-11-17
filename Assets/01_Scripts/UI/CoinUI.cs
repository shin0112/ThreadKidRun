using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [Header("코인")]
    [SerializeField] private TextMeshProUGUI _coin;

    public void UpdateCoinText(int value)
    {
        _coin.text = value.ToString();
    }
}
