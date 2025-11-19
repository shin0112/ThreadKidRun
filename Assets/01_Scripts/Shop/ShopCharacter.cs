using GameName.Data;
using UnityEngine;

public class ShopCharacter : MonoBehaviour
{
    [SerializeField] private CharacterSkinData _data;

    public int GetPriceValue()
    {
        Logger.Log($"가격: {_data.unlockCost}");
        return _data.unlockCost;
    }

    public bool CheckSold()
    {
        return _data.isUnlocked;
    }
}
