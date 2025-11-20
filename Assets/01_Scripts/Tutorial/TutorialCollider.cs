using System;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public event Action OnEnterTrigger;
    public event Action OnExitTrigger;
    public int Index { get; set; }

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

            if (Index == Define.TutorialCount - 1)
            {
                GameManager.Instance.EndTutorial();
            }
        }
    }

    private void OnDestroy()
    {
        OnEnterTrigger = null;
        OnExitTrigger = null;
    }
}
