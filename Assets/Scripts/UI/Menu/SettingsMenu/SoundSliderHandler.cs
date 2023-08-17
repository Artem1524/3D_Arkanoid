using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Arkanoid.Managers;

public class SoundSliderHandler : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private void Start()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
        _slider.value = SettingsManager.GetInstance().GetSoundVolume();
    }

    public void OnValueChanged(float value)
    {
        SettingsManager.GetInstance().SaveSoundVolume(value);
    }
}
