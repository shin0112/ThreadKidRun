using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IUIActive
{
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _get;

    [Header("구입 버튼")]
    [SerializeField] private GameObject _buttonIcon;
    [SerializeField] private TextMeshProUGUI _price;

    private int _curSelectIndex;

    private ShopController _controller;

    private void Start()
    {
        _controller = FindObjectOfType<ShopController>(true);
        if (_controller == null)
        {
            Logger.LogWarning("shop controller is null");
            return;
        }

        InitShopController();
        _get.onClick.AddListener(GetSkin);

        _curSelectIndex = GameManager.Instance.CurSkinIndex;
    }

    private void OnDestroy()
    {
        _leftArrow.onClick.RemoveAllListeners();
        _rightArrow.onClick.RemoveAllListeners();
        _get.onClick.RemoveAllListeners();
    }

    public void UpdateButtonText(CharacterSlot shopCharacter)
    {
        int price = shopCharacter.GetPriceValue();
        bool selected = _curSelectIndex == shopCharacter.Index;

        if (price == 0 || shopCharacter.CheckSold() || selected)
        {
            _buttonIcon.SetActive(false);
            _price.rectTransform.sizeDelta = new Vector2(Define.SoldTextWidth, Define.SoldTextHeight);
            _price.text = selected ? "선택 중" : "선택 가능";
            return;
        }

        _price.rectTransform.sizeDelta = new Vector2(Define.GoldTextWidth, Define.GoldTextHeight);
        _buttonIcon.SetActive(true);
        _price.text = price.ToString();
    }

    private void GetSkin()
    {
        CharacterSlot selected = _controller.GetSkin();

        // 스킨 인덱스 변경
        GameManager.Instance.CurSkinIndex = selected.Index;
        _curSelectIndex = selected.Index;

        UpdateButtonText(selected);
    }

    private void InitShopController()
    {
        Logger.Log("shop controller 초기화");
        _controller = FindObjectOfType<ShopController>(true);

        _leftArrow.onClick.RemoveAllListeners();
        _rightArrow.onClick.RemoveAllListeners();

        _leftArrow.onClick.AddListener(_controller.RotateLeft);
        _rightArrow.onClick.AddListener(_controller.RotateRight);
    }

    #region 인터페이스 구현
    public void SetDefaultMode()
    {
        _leftArrow.gameObject.SetActive(false);
        _rightArrow.gameObject.SetActive(false);
        _get.gameObject.SetActive(false);

        if (_controller == null)
        {
            InitShopController();
        }

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

        if (_controller == null)
        {
            InitShopController();
        }

        _controller.ShopCamera.gameObject.SetActive(true);
        _controller.gameObject.SetActive(true);
    }
    #endregion
}
