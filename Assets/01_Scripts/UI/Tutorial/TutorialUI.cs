using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour, IUIActive
{
    [SerializeField] private List<TutorialContainer> _tutorialObjects;

    private void Reset()
    {
        _tutorialObjects.Add(transform.FindChild<TutorialContainer>("Move"));
        _tutorialObjects.Add(transform.FindChild<TutorialContainer>("Jump"));
        _tutorialObjects.Add(transform.FindChild<TutorialContainer>("Slide"));
        _tutorialObjects.Add(transform.FindChild<TutorialContainer>("Item"));
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        var tutorialController = FindObjectOfType<TutorialController>(true);
        List<TutorialCollider> colliders = tutorialController.Colliders;

        for (int i = 0; i < colliders.Count; i++)
        {
            var collider = colliders[i];
            collider.Index = i;
            collider.OnEnterTrigger += _tutorialObjects[i].HandleEnterTrigger;
            collider.OnExitTrigger += _tutorialObjects[i].HandleExitTrigger;
        }
    }

    #region 인터페이스 구현
    public void SetMode(UIMode mode)
    {
        if (GameManager.Instance.FinishedTutorial) return;

        switch (mode)
        {
            case UIMode.Default:
                SetDefaultMode();
                break;
        }
    }

    private void SetDefaultMode()
    {
        _tutorialObjects.ForEach(t => t.gameObject.SetActive(false));
    }
    #endregion
}
