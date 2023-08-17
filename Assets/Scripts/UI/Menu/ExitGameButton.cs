using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitGameButton : AnimatedButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        UnityEditor.EditorApplication.isPaused = true;
    }
}
