using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Fog")] 
    [SerializeField] private Volume _volume;
    [SerializeField] private Slider _fogSlider;
    [SerializeField] private Text _fogToggleButtonText;
    private Fog _fog;

    [Header("Day&Night Toggle")]
    [SerializeField] private Light _directionalLight;
    [SerializeField] private Cubemap _skyboxDay;
    [SerializeField] private Cubemap _skyboxNight;
    [SerializeField] private Text _timeToggleButtonText;
    private HDRISky _HDRISky;

    private void Awake()
    {
        if (_volume.profile.TryGet(out Fog tempFog))
        {
            _fog = tempFog;
        }

        if(_fog.active)
        {
            _fogToggleButtonText.text = "Fog On";
        }
        else
        {
            _fogToggleButtonText.text = "Fog Off";
        }

        if (_volume.profile.TryGet(out HDRISky tempHDRISky))
        {
            _HDRISky = tempHDRISky;
        }

        if(_HDRISky.hdriSky == _skyboxDay)
        {
            _timeToggleButtonText.text = "Day";
        }
        else
        {
            _timeToggleButtonText.text = "Night";
        }

        _fogSlider.maxValue = 40;
        _fogSlider.minValue = 0;
        _fogSlider.value = _fog.meanFreePath.value;
    }

    public void ChangeFog()
    {
        _fog.meanFreePath.value = 100 - _fogSlider.value;
    }

    public void FogOnOff()
    {
        if (_fog.active)
        {
            _fog.active = false;
            _fogToggleButtonText.text = "Fog Off";
        }

        else
        {
            _fog.active = true;
            _fogToggleButtonText.text = "Fog On";
        }
    }

    public void DayNightCircle()
    {
        if (_HDRISky.hdriSky == _skyboxNight)
        {
            _HDRISky.hdriSky.value = _skyboxDay;
            _directionalLight.intensity = 5000;
            _timeToggleButtonText.text = "Day";
        }

        else
        {
            _HDRISky.hdriSky.value = _skyboxNight;
            _directionalLight.intensity = 2500;
            _timeToggleButtonText.text = "Night";
        }
    }
}
