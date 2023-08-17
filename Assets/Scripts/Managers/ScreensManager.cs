using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Managers
{
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField]
        private CustomCanvas _menuPause1;
        [SerializeField]
        private CustomCanvas _menuPause2;
        [SerializeField]
        private CustomCanvas _menuSettings1;
        [SerializeField]
        private CustomCanvas _menuSettings2;

        [SerializeField]
        private CustomCanvas _mainMenu1;
        [SerializeField]
        private CustomCanvas _mainMenu2;

        private bool _isFromMainMenu;

        private static ScreensManager _self;

        private void Start()
        {
            _self = this;
        }

        public void ShowPauseMenu()
        {
            _menuPause1.gameObject.SetActive(true);
            _menuPause2.gameObject.SetActive(true);
        }

        public void ClosePauseMenu()
        {
            _menuPause1.gameObject.SetActive(false);
            _menuPause2.gameObject.SetActive(false);

            ResetButtonsScale(_menuPause1);
            ObjectsManager.GetInstance().EnablePlayersControls();
            ObjectsManager.GetInstance().UnPauseBallMoving();
        }

        public void ShowSettingsMenu(bool isFromMainMenu)
        {
            _isFromMainMenu = isFromMainMenu;

            if (isFromMainMenu)
            {
                _mainMenu1.gameObject.SetActive(false);
                _mainMenu2.gameObject.SetActive(false);
                ResetButtonsScale(_mainMenu1);
            }
            else
            {
                _menuPause1.gameObject.SetActive(false);
                _menuPause2.gameObject.SetActive(false);
                ResetButtonsScale(_menuPause1);
            }

            _menuSettings1.gameObject.SetActive(true);
            _menuSettings2.gameObject.SetActive(true);
        }

        public void CloseSettingsMenu()
        {
            _menuSettings1.gameObject.SetActive(false);
            _menuSettings2.gameObject.SetActive(false);
            ResetButtonsScale(_menuSettings1);

            if (_isFromMainMenu)
            {
                _mainMenu1.gameObject.SetActive(true);
                _mainMenu2.gameObject.SetActive(true);
            }
            else
            {
                _menuPause1.gameObject.SetActive(true);
                _menuPause2.gameObject.SetActive(true);
            }
        }

        private void ResetButtonsScale(CustomCanvas customCanvas)
        {
            AnimatedButton[] buttons = customCanvas.GetComponentsInChildren<AnimatedButton>();

            foreach (AnimatedButton button in buttons)
                button.ResetScale();
        }

        public static ScreensManager GetInstance()
        {
            return _self;
        }
    }
}