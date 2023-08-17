using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;

using Arkanoid.GameObjects;

namespace Arkanoid {
    public class PlayerControllerBase : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 5f)]
        [Tooltip("Скорость платформы без учёта инерции")]
        protected float _speed = 2f;

        [SerializeField, Range(1f, 100f)]
        [Tooltip("Сила инерции в процентах")]
        protected float _inertiaPower = 60f;

        [SerializeField, Range(1f, 500f)]
        [Tooltip("Сила уменьшения инерции (в процентах)")]
        protected float _inertiaDecreasingPower = 10f;

        [SerializeField, Range(0.1f, 30f)]
        [Tooltip("Длительность набора и действия инерции (в секундах)")]
        protected float _timeToMaxInertia = 1f;

        private Vector3 _inertiaVector = new Vector3(0, 0, 0);
        private Vector3 _motionVector = new Vector3(0, 0, 0);

        protected PlayerControls _controls;
        protected Ball _ball;
        protected Platform _platform;

        public void ResetInertia()
        {
            StopCoroutine(MoveWithInertia());
            _inertiaVector = new Vector3(0, 0, 0);
            _motionVector = new Vector3(0, 0, 0);
        }

        public void EnableControlsMap()
        {
            _controls.Map.Enable();
        }

        public void DisableControlsMap()
        {
            _controls.Map.Disable();
        }

        protected void Update()
        {
            Move();
        }

        protected void OnPlayerStartMoving(CallbackContext context)
        {
            StopCoroutine(MoveWithInertia());
        }

        protected void OnPlayerMove(CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            float x = value.x;
            float y = value.y;

            SetMotion(x, y);
        }

        protected void OnPlayerStopMoving(CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            float x = value.x;
            float y = value.y;

            if (x == 0 && y == 0)
            {
                StopMotion();
                StartCoroutine(MoveWithInertia());
            }
        }

        protected void SetMotion(float x, float y)
        {
            _motionVector.x = x;
            _motionVector.z = y;
        }

        protected void StopMotion()
        {
            _motionVector.x = _motionVector.z = 0;
        }

        private void Move() {
            _platform.transform.position += _motionVector * _speed * Time.deltaTime;

            _inertiaVector += new Vector3(_motionVector.x, 0, _motionVector.z) * Time.deltaTime / _timeToMaxInertia;

            if (_inertiaVector.magnitude > 1)
            {
                _inertiaVector.Normalize();
            }
        }

        protected IEnumerator MoveWithInertia()
        {
            while (_inertiaVector.magnitude > 0.1f) {
                _platform.transform.position += _inertiaVector * (_inertiaPower / 100) * Time.deltaTime * _timeToMaxInertia;
                _inertiaVector -= _inertiaVector * (_inertiaDecreasingPower / 100) * Time.deltaTime;
                yield return null;
            }

            _inertiaVector.x = _inertiaVector.z = 0;
            yield break;
        }
    }
}