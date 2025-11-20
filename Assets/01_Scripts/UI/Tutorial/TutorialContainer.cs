using UnityEngine;

public class TutorialContainer : MonoBehaviour
{
    internal void HandleEnterTrigger()
    {
        gameObject.SetActive(true);
    }

    internal void HandleExitTrigger()
    {
        gameObject.SetActive(false);
    }
}
