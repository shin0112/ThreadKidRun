using UnityEngine;

public class ShopUI : MonoBehaviour, IUIActive
{
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
