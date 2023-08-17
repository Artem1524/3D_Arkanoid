using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

using Arkanoid.GameObjects;
using Arkanoid.Managers;

namespace Arkanoid
{
    public class Player1Controller : PlayerControllerBase
    {
        private void Awake()
        {
            _controls = new PlayerControls();
            _ball = FindObjectOfType<Ball>();
            _platform = FindObjectOfType<Platform1>();
        }

        private void OnEnable()
        {
            _controls = new PlayerControls();
            _controls.Map.Enable();

            _controls.Map.PushBall.performed += OnPushBall;
            _controls.Map.Player1Move.performed += OnPlayerMove;
            _controls.Map.Player1Move.started += OnPlayerStartMoving;
            _controls.Map.Player1Move.canceled += OnPlayerStopMoving;
            _controls.Map.Pause.performed += OnShowPauseMenu;

            _controls.Map.Player2Move.Disable();
        }

        private void OnPushBall(CallbackContext context)
        {
            if (!_ball.IsMoving)
            {
                PushBall();
            }
        }

        private void PushBall()
        {
            _ball.StartMove();
        }

        private void OnShowPauseMenu(CallbackContext context)
        {
            ScreensManager.GetInstance().ShowPauseMenu();

            ObjectsManager.GetInstance().DisablePlayersControls();
            ObjectsManager.GetInstance().PauseBallMoving();
            ObjectsManager.GetInstance().ResetPlayersInertia();
        }
    }
}
