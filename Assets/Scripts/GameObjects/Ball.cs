using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Arkanoid.Managers;

namespace Arkanoid.GameObjects
{
    public class Ball : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 5f)]
        [Tooltip("Базовая скорость шара")]
        private float _baseSpeed = 2f;
        [SerializeField, Range(0.5f, 2f)]
        [Tooltip("Увеличение скорости шара при столкновении с препятствиями")]
        private float _speedIncrease = 1f;
        [SerializeField, Range(0.5f, 10f)]
        [Tooltip("Максимальная скорость шара")]
        private float _maxSpeed = 5f;

        private float _speed;
        private Vector3 _direction;

        public bool IsMoving { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint contact = collision.GetContact(0);
            Vector3 pos = contact.normal;
            _direction = Vector3.Reflect(_direction, pos);
            _direction.Normalize();

            IncreaseSpeed();

            Block block = null;
            if (collision.gameObject.TryGetComponent<Block>(out block))
            {
                LevelManager.GetInstance().BlockDestroyed();
            }
        }

        public void StartMove()
        {
            StartCoroutine(Moving());
            IsMoving = true;
        }

        public void Reset()
        {
            _direction = -1 * transform.up;
            _speed = _baseSpeed;
            StopMoving();
        }

        public void PauseMove()
        {
            StopAllCoroutines();
        }

        public void UnPauseMoving()
        {
            if (IsMoving)
                StartMove();
        }

        private void StopMoving()
        {
            IsMoving = false;
            StopAllCoroutines();
        }

        private IEnumerator Moving()
        {
            while (true)
            {
                transform.position += _direction * _speed * Time.deltaTime;
                yield return null;
            }
        }

        private void IncreaseSpeed()
        {
            _speed = Mathf.Clamp(_speed + _speedIncrease, _baseSpeed, _maxSpeed);
        }
    }
}