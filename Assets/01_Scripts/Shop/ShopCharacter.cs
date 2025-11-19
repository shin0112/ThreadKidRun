using GameName.Data;
using UnityEngine;

public class ShopCharacter : MonoBehaviour
{
    [SerializeField] private CharacterSkinData _data;

    public int GetPriceValue()
    {
        return _data.unlockCost;
    }
}
