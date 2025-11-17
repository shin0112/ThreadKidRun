using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Transform _characterContainer;
    [SerializeField] private List<GameObject> _characters;
    [SerializeField] private Camera _shopCamera;

    public Camera ShopCamera => _shopCamera;

    public void RotateRight()
    {
        Vector3 rotation = _characterContainer.localEulerAngles + new Vector3(0, 60f, 0f);
        _characterContainer.localEulerAngles = rotation;
    }

    public void RotateLeft()
    {
        Vector3 rotation = _characterContainer.localEulerAngles + new Vector3(0, -60f, 0f);
        _characterContainer.localEulerAngles = rotation;
    }
}
