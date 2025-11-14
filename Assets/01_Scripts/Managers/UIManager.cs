using UnityEngine;

public enum SceneType
{
    LoadScene,
    GameScene,
    StartScene,
}

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private LoadingUI _loading;

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

    /// <summary>
    /// 씬 전환하기
    /// </summary>
    /// <param name="type"></param>
    public void LoadScene(SceneType type)
    {
        _loading.gameObject.SetActive(true);
        _loading.LoadScene(type);
    }
}
