using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IUIActive
{
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _get;
    [SerializeField] private TextMeshProUGUI _price;

    ShopController _controller;

    private void Start()
    {
        _controller = FindAnyObjectByType<ShopController>();
        if (_controller == null)
        {
            Logger.Log("shop controller is null");
            return;
        }

        _leftArrow.onClick.AddListener(_controller.RotateLeft);
        _rightArrow.onClick.AddListener(_controller.RotateRight);
        // todo: get 버튼 구현

        _controller.OnChangedPriceText += UpdatePriceText;
    }

    private void OnDestroy()
    {
        _leftArrow.onClick.RemoveAllListeners();
        _rightArrow.onClick.RemoveAllListeners();
        _get.onClick.RemoveAllListeners();

        _controller.OnChangedPriceText -= UpdatePriceText;
    }

    private void UpdatePriceText(int price)
    {
        _price.text = price.ToString();
    }

    #region 인터페이스 구현
    public void SetDefaultMode()
    {
        _leftArrow.gameObject.SetActive(false);
        _rightArrow.gameObject.SetActive(false);
        _get.gameObject.SetActive(false);
        _controller.ShopCamera.gameObject.SetActive(false);
        _controller.gameObject.SetActive(false);
    }

    public void SetGameMode()
    {
    }

    public void SetShopMode()
    {
        _leftArrow.gameObject.SetActive(true);
        _rightArrow.gameObject.SetActive(true);
        _get.gameObject.SetActive(true);
        _controller.ShopCamera.gameObject.SetActive(true);
        _controller.gameObject.SetActive(true);
    }
    #endregion
}
