using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("캐릭터")]
    [SerializeField] private Transform _characterContainer;
    [SerializeField] private List<GameObject> _characters;

    [SerializeField] private Camera _shopCamera;
    public Camera ShopCamera => _shopCamera;

    [Header("회전")]
    [SerializeField] private float _rotateDuration = 1f;
    IEnumerator _rotateCoroutine;

    // 캐릭터 리스트 관리
    private int _curSelectIndex = 0;

    private void Awake()
    {
        if (_characters.Count != Define.CharacterCount)
        {
            Logger.Log("캐릭터 개수 불일치");
        }
    }

    public void RotateRight()
    {
        StartRotate(60f);

        _curSelectIndex = (_curSelectIndex + 1) % Define.CharacterCount;
        Logger.Log($"현재 캐릭터 번호: {_curSelectIndex}");
    }

    public void RotateLeft()
    {
        StartRotate(-60f);

        _curSelectIndex = (_curSelectIndex + Define.CharacterCount - 1) % Define.CharacterCount;
        Logger.Log($"현재 캐릭터 번호: {_curSelectIndex}");
    }

    private void StartRotate(float value)
    {
        Vector3 rotation = _characterContainer.localEulerAngles + new Vector3(0, value, 0);
        _characterContainer.localEulerAngles = rotation;
    }

    private IEnumerator RotateCoroutine(float value)
    {
        float elapseed = 0f;

        float startY = _characterContainer.localEulerAngles.y;
        float endY = startY + value;

        yield return null;
    }
}
