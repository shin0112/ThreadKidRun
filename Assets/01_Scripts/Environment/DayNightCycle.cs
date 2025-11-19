using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _time;
    [SerializeField] private float _fullDayLength;
    [SerializeField] private float _startTime = 0.4f;
    private float _timeRate;
    [SerializeField] private Vector3 _noon;

    [Header("Sun")]
    [SerializeField] Light _sun;
    [SerializeField] Gradient _sunGrad;
    [SerializeField] AnimationCurve _sunIntensity;

    [Header("Moon")]
    [SerializeField] Light _moon;
    [SerializeField] Gradient _moonGrad;
    [SerializeField] AnimationCurve _moonIntensity;

    [Header("Other Lighting")]
    [SerializeField] private AnimationCurve _lightingIntensityMultiplier;
    [SerializeField] private AnimationCurve _reflectionIntensityMultiplier;

    private void Start()
    {
        _timeRate = 1.0f / _fullDayLength;
        _time = _startTime;                 // 특정 시점 부터 시작(ex. 점심)
    }

    private void Update()
    {
        _time = (_time + _timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(_sun, _sunGrad, _sunIntensity);
        UpdateLighting(_moon, _moonGrad, _moonIntensity);

        RenderSettings.ambientIntensity = _lightingIntensityMultiplier.Evaluate(_time);
        RenderSettings.reflectionIntensity = _reflectionIntensityMultiplier.Evaluate(_time);
        //DynamicGI.UpdateEnvironment();
    }

    private void UpdateLighting(Light light, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(_time);

        light.transform.eulerAngles = (_time - (light == _sun ? 0.25f : 0.75f)) * _noon * 4.0f;
        light.color = gradient.Evaluate(_time);
        light.intensity = intensity;

        GameObject go = light.gameObject;
        if (light.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if (light.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
