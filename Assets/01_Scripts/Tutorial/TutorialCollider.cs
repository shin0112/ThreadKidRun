using System;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public event Action OnEnterTrigger;
    public event Action OnExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Logger.Log("플레이어 입장");
            OnEnterTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Logger.Log("플레이어 통과");
            OnExitTrigger?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnEnterTrigger = null;
        OnExitTrigger = null;
    }
}
