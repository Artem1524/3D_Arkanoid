using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Arkanoid.Managers;

public class NewGameButton : AnimatedButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        CustomSceneManager.GetInstance().StartNewGame();
    }
}
