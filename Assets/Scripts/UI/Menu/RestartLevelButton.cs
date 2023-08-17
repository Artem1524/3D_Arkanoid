using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Arkanoid.Managers;

public class RestartLevelButton : AnimatedButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.GetInstance().RestartLevel();
        ScreensManager.GetInstance().ClosePauseMenu();
    }
}
