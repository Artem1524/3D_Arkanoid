using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

using Arkanoid.GameObjects;

namespace Arkanoid
{
    public class Player2Controller : PlayerControllerBase
    {
        private void Awake()
        {
            _controls = new PlayerControls();
            _ball = FindObjectOfType<Ball>();
            _platform = FindObjectOfType<Platform2>();
        }

        private void OnEnable()
        {
            _controls = new PlayerControls();
            _controls.Map.Enable();

            _controls.Map.Player2Move.performed += OnPlayerMove;
            _controls.Map.Player2Move.started += OnPlayerStartMoving;
            _controls.Map.Player2Move.canceled += OnPlayerStopMoving;

            _controls.Map.Player1Move.Disable();
            _controls.Map.PushBall.Disable();
        }
    }
}