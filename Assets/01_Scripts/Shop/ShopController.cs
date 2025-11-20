using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("캐릭터")]
    [SerializeField] private Transform _charactersContainer;
    [SerializeField] private List<CharacterSlot> _characterSlots;
    // todo: 캐릭터 가격 연동하기

    [SerializeField] private Camera _shopCamera;
    public Camera ShopCamera => _shopCamera;

    [Header("회전")]
    [SerializeField] private float _rotateDuration = 0.5f;
    private IEnumerator _rotateCoroutine;
    public event Action<CharacterSlot> OnChangedPriceText;

    // 캐릭터 리스트 관리
    private int _curSelectIndex = 0;
    public int CurSelectIndex => _curSelectIndex;

    private void Awake()
    {
        if (_characterSlots.Count != Define.CharacterCount)
        {
            Logger.Log("캐릭터 개수 불일치");
        }

        _shopCamera = GetComponentInChildren<Camera>(true);
    }

    private void OnEnable()
    {
        OnChangedPriceText += UIManager.Instance.ShopUI.UpdateButtonText;
        OnChangedPriceText?.Invoke(_characterSlots[_curSelectIndex]);
    }

    private void Start()
    {
        _characterSlots.ForEach((slot) => slot.Init());
    }

    private void OnDisable()
    {
        OnChangedPriceText += UIManager.Instance.ShopUI.UpdateButtonText;
        if (_rotateCoroutine != null) StopAllCoroutines();
    }

    #region 상점 회전
    /// <summary>
    /// 우측으로 60도 회전
    /// </summary>
    public void RotateRight()
    {
        _curSelectIndex = (_curSelectIndex + 1) % Define.CharacterCount;
        Logger.Log($"현재 캐릭터 위치 번호: {_curSelectIndex}");

        StartRotate();
    }

    /// <summary>
    /// 좌측으로 60도 회전
    /// </summary>
    public void RotateLeft()
    {
        _curSelectIndex = (_curSelectIndex + Define.CharacterCount - 1) % Define.CharacterCount;
        Logger.Log($"현재 캐릭터 번호: {_curSelectIndex}");

        StartRotate();
    }

    private void StartRotate()
    {
        // 진행 중인 코루틴이 있을 경우 정지
        if (_rotateCoroutine != null)
        {
            StopCoroutine(_rotateCoroutine);
        }

        float targetAngle = _curSelectIndex * 60f;
        _rotateCoroutine = RotateCoroutine(targetAngle);
        StartCoroutine(_rotateCoroutine);
    }

    private IEnumerator RotateCoroutine(float value)
    {
        float elapsed = 0f;

        Quaternion start = _charactersContainer.transform.localRotation;
        Quaternion end = Quaternion.Euler(0, value, 0);

        while (elapsed < _rotateDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _rotateDuration;
            _charactersContainer.localRotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }

        _charactersContainer.localRotation = end;

        OnChangedPriceText?.Invoke(_characterSlots[_curSelectIndex]);
    }
    #endregion

    public CharacterSlot GetSkin()
    {
        CharacterSlot selected = _characterSlots[_curSelectIndex];

        if (!selected.CheckSold() &&
            GameManager.Instance.CheckSpendCoinAndGetSkin(selected.GetPriceValue()))
        {
            Logger.Log($"{selected.name} 구매 완료");
            selected.GetSkin();
            GameManager.Instance.EarnCharacter(1);
        }
        return selected;
    }
}
