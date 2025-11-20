using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomizingController : MonoBehaviour
{
    [SerializeField] private List<CharacterSkinContainer> _skinData;

    private void Awake()
    {
        _skinData = GetComponentsInChildren<CharacterSkinContainer>().ToList();
    }

    private void Start()
    {
        // todo: select skin 데이터 불러오기
        SelectSkin(GameManager.Instance.CurSkinIndex);
        GameManager.Instance.OnSkinIndexChanged += SelectSkin;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSkinIndexChanged -= SelectSkin;
    }

    public void SelectSkin(int selectIndex)
    {
        foreach (var skin in _skinData)
        {
            skin.gameObject.SetActive(skin.SkinIndex == selectIndex);
        }
    }
}
