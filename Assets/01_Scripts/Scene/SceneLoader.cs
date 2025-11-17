using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneType
{
    LoadScene,
    GameScene,
    StartScene,
}

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    public static SceneLoader Instance => _instance;

    private SceneType _curScene = SceneType.LoadScene;

    [Header("로딩 UI")]
    [SerializeField] private GameObject _loading;
    [SerializeField] private Image _progress;
    [SerializeField] private Image _character;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadScene(SceneType.StartScene);
    }

    public void LoadScene(SceneType type)
    {
        if (type == _curScene) return;

        StartCoroutine(LoadSceneCoroutine(type));
        _curScene = type;
    }

    private IEnumerator LoadSceneCoroutine(SceneType type)
    {
        _loading.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(type.ToString());

        _progress.fillAmount = 0f;
        float progress = 0f;
        float endX = Define.ProgressCharacterEndX * 0.95f;
        float originY = _character.rectTransform.position.y;
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

        _loading.SetActive(false);
    }

    #region 로딩씬 Test용 코드
    //float _loadingtime = 0f;
    //bool _isLoaded = false;
    //private void Update()
    //{
    //    if (_loadingtime > 1f && !_isLoaded)
    //    {
    //        LoadScene(SceneType.StartScene);
    //        _isLoaded = true;
    //    }
    //    _loadingtime += Time.deltaTime;
    //}
    #endregion
}
