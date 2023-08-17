using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Arkanoid.Managers;

namespace Arkanoid.GameObjects {
    public class BallExitRegion : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            LevelManager.GetInstance().BallExit();
        }
    } }