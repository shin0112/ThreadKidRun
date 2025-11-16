using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header("음향 시스템")]
    [SerializeField] private Slider _soundEffect;
    [SerializeField] private Slider _backgroundSound;

    [Header("버튼")]
    [SerializeField] private Button _exitGame;

    private void OnEnable()
    {
        _exitGame.onClick.AddListener(ExitGame);
        _soundEffect.onValueChanged.AddListener(SetSoundEffecVolume);
        _backgroundSound.onValueChanged.AddListener(SetBgSoundVolume);
    }

    private void OnDisable()
    {
        _exitGame.onClick.RemoveAllListeners();
        _soundEffect.onValueChanged.RemoveAllListeners();
        _backgroundSound.onValueChanged.RemoveAllListeners();
    }

    /// <summary>
    /// 효과음 설정하기
    /// </summary>
    /// <param name="volume"></param>
    private void SetSoundEffecVolume(float volume)
    {
        // todo: 오디오 믹서와 연결
        Logger.Log($"효과음 크기: {volume}");
    }

    /// <summary>
    /// 배경음악 설정하기
    /// </summary>
    /// <param name="volume"></param>
    private void SetBgSoundVolume(float volume)
    {
        // todo: 오디오 믹서와 연결
        Logger.Log($"배경음악 크기: {volume}");
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
