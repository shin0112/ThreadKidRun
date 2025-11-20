using UnityEngine;

public class SkyBox : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _time;
    [SerializeField] private float _fullDayLength;
    [SerializeField] private float _startTime = 0.4f;
    private float _timeRate;
    [SerializeField] private Vector3 _noon;

    [Header("Sun")]
    [SerializeField] Light _sun;

    private void Start()
    {
        _timeRate = 1.0f / _fullDayLength;
        _time = _startTime;                 // 특정 시점 부터 시작(ex. 점심)
    }

    private void Update()
    {
        _time = (_time + _timeRate * Time.deltaTime) % 1.0f;

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.4f);
    }
}
