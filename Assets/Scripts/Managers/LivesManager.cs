using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Managers
{
    public class LivesManager : MonoBehaviour
    {
        [SerializeField, Range(1, 30)]
        [Tooltip("Общее количество жизней у игроков")]
        private int _lives = 3;

        private static LivesManager _livesManager;
        
        private void Start()
        {
            _livesManager = this;
        }

        public bool IsLivesLeft()
        {
            return _lives <= 0;
        }

        public void DecreaseLive()
        {
            _lives--;

            UIManager.GetInstance().UpdateLifeInfo(_lives);

            if (_lives > 0)
            {
                Debug.LogWarning($"Шар ушёл. Осталось жизней: {_lives}");
            }
        }

        public static LivesManager GetInstance()
        {
            return _livesManager;
        }
    }
}