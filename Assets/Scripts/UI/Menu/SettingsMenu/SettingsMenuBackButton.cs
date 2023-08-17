using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Arkanoid.Managers;

public class SettingsMenuBackButton : AnimatedButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        ScreensManager.GetInstance().CloseSettingsMenu();
    }
}
