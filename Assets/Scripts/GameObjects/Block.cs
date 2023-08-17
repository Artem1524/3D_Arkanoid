using System;
using UnityEngine;

namespace Arkanoid.GameObjects
{
    public class Block : MonoBehaviour
    {
        public event Action OnDestroy;

        private void OnCollisionEnter(Collision collision)
        {
            OnDestroy?.Invoke();
        }
    }
}