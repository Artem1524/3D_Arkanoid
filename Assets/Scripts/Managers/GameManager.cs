using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _gameManager;

        private void Start()
        {
            _gameManager = this;
            StartFirstLevel();
        }

        private void StartFirstLevel()
        {
            LevelManager.GetInstance().StartFirstLevel();
        }

        public void FinishGame()
        {
            Debug.LogWarning("Вы проиграли");
            UnityEditor.EditorApplication.isPaused = true;
        }

        public static GameManager GetInstance()
        {
            return _gameManager;
        }
    }
}