using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IUIActive
{
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;

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
    }

    private void OnDestroy()
    {
        _leftArrow.onClick.RemoveAllListeners();
        _rightArrow.onClick.RemoveAllListeners();
    }

    #region 인터페이스 구현
    public void SetDefaultMode()
    {
        gameObject.SetActive(false);
    }

    public void SetGameMode()
    {
        gameObject.SetActive(false);
    }

    public void SetShopMode()
    {
        gameObject.SetActive(true);
    }
    #endregion
}
