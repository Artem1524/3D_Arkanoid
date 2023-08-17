using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Arkanoid.Managers;

public class TurnOffSoundCheckboxHandler : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool isOn)
    {
        if (!isOn)
            SettingsManager.GetInstance().SaveSoundVolume(0);
    }
}
