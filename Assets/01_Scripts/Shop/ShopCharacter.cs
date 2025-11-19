using GameName.Data;
using UnityEngine;

public class ShopCharacter : MonoBehaviour
{
    [SerializeField] private CharacterSkinData _data;
    [SerializeField] private bool _isSold;

    public int GetPriceValue()
    {
        Logger.Log($"가격: {_data.unlockCost}");
        return _data.unlockCost;
    }

    public bool CheckSold()
    {
        // todo: 구매했는지 아닌지 확인
        return _isSold;
    }
}
