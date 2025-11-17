using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    // 컴포넌트
    [SerializeField] private Transform _characterContainer;
    [SerializeField] private List<GameObject> _characters;
    [SerializeField] private Camera _shopCamera;
    public Camera ShopCamera => _shopCamera;

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
        Vector3 rotation = _characterContainer.localEulerAngles + new Vector3(0, 60f, 0f);
        _characterContainer.localEulerAngles = rotation;

        _curSelectIndex = (_curSelectIndex + 1) % Define.CharacterCount;
        Logger.Log($"현재 캐릭터 번호: {_curSelectIndex}");
    }

    public void RotateLeft()
    {
        Vector3 rotation = _characterContainer.localEulerAngles + new Vector3(0, -60f, 0f);
        _characterContainer.localEulerAngles = rotation;

        _curSelectIndex = (_curSelectIndex + Define.CharacterCount - 1) % Define.CharacterCount;
        Logger.Log($"현재 캐릭터 번호: {_curSelectIndex}");
    }
}
