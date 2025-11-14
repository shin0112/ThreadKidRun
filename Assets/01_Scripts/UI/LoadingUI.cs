using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Image _progress;
    [SerializeField] private Image _character;

    public void LoadScene(SceneType type)
    {
        StartCoroutine(LoadSceneCoroutine(type));
    }

    private IEnumerator LoadSceneCoroutine(SceneType type)
    {
        _progress.fillAmount = 0f;
        float progress = 0f;
        float endX = Define.ProgressCharacterEndX * 0.95f;
        float originY = _character.rectTransform.position.y;
        AsyncOperation async = SceneManager.LoadSceneAsync(type.ToString());

        while (!async.isDone)
        {
            progress = async.progress / 0.95f;
            _progress.fillAmount = Mathf.Clamp01(progress);
            float x = Mathf.Lerp(0f, endX, progress);
            _character.rectTransform.position = new Vector2(x, originY);
            yield return null;
        }

        _progress.fillAmount = 1f;
        _character.rectTransform.position = new Vector2(Define.ProgressCharacterEndX, originY);

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
