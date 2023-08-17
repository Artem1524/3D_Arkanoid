using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyListHandler : MonoBehaviour
{
    [SerializeField]
    private Dropdown _dropdown;

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(int value)
    {
        Debug.Log("Сложность: " + _dropdown.options[value].text);
    }
}
