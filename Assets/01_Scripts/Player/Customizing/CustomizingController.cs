using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomizingController : MonoBehaviour
{
    [SerializeField] private List<CharacterSkinContainer> _skinData;

    private PlayerCollider _playerCollider;
    private PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _skinData = GetComponentsInChildren<CharacterSkinContainer>().ToList();

        if (!TryGetComponent(out _playerCollider)) Logger.LogWarning("콜라이더 초기화 안됨");
        if (!TryGetComponent(out _playerAnimation)) Logger.LogWarning("애니메이션 초기화 안됨");
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

        UpdateAnimator();
    }

    public Animator GetAnimator()
    {
        int index = GameManager.Instance.CurSkinIndex;

        if (_skinData[index].TryGetComponent<Animator>(out var animator))
        {
            return animator;
        }

        Logger.LogWarning("애니메이터 없음");
        return null;
    }

    public void UpdateAnimator()
    {
        Animator anim = GetAnimator();

        _playerCollider.Animator = anim;
        _playerAnimation.UpdateAnimator(anim);

        Logger.Log("애니메이션 업데이트");
    }
}
