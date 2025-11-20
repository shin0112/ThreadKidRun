using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<TutorialCollider> _colliders;
    public List<TutorialCollider> Colliders => _colliders;

    private void Reset()
    {
        _colliders.Add(transform.FindChild<TutorialCollider>("Move"));
        _colliders.Add(transform.FindChild<TutorialCollider>("Jump"));
        _colliders.Add(transform.FindChild<TutorialCollider>("Slide"));
        _colliders.Add(transform.FindChild<TutorialCollider>("Item"));
    }

    private void Start()
    {
        if (GameManager.Instance.FinishedTutorial)
        {
            gameObject.SetActive(false);
        }
    }
}
