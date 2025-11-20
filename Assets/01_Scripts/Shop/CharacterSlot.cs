using GameName.Data;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private int _index;
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

    public string GetName()
    {
        return _data.name;
    }

    public void IsUnlocked()
    {
        _data.isUnlocked = true;
    }

    public bool CheckUnlocked()
    {
        if (_data.isUnlocked)
        {
            Logger.Log("이미 구매한 스킨");
        }
        return _data.isUnlocked;
    }
}
