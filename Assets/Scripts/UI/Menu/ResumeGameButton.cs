using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Arkanoid.Managers;

public class ResumeGameButton : AnimatedButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        ScreensManager.GetInstance().ClosePauseMenu();
    }
}
