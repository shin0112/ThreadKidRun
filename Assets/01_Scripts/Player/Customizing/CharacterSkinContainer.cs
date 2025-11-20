using GameName.Data;
using UnityEngine;

public class CharacterSkinContainer : MonoBehaviour
{
    [SerializeField] private CharacterSkinData _skinData;

    public int SkinIndex => _skinData.skinID;
}
