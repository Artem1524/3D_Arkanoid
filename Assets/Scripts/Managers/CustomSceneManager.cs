using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Managers
{
    public class CustomSceneManager : MonoBehaviour
    {
        private static CustomSceneManager _self;

        private void Start()
        {
            _self = this;
            
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene("Scenes/Game");
        }

        public static CustomSceneManager GetInstance()
        {
            return _self;
        }
    }
}
