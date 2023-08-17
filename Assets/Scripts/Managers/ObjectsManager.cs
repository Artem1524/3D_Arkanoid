using UnityEngine;

using Arkanoid.GameObjects;

namespace Arkanoid.Managers
{
    public class ObjectsManager : MonoBehaviour
    {
        private Ball _ball;
        private Platform1 _platform1;
        private Platform2 _platform2;

        private BallSpawner _ballSpawner;
        private PlatformSpawner1 _platformSpawner1;
        private PlatformSpawner2 _platformSpawner2;

        private Player1Controller _playerController1;
        private Player2Controller _playerController2;

        private static ObjectsManager _objectsManager;

        private void Awake()
        {
            _ball = FindObjectOfType<Ball>();
            _platform1 = FindObjectOfType<Platform1>();
            _platform2 = FindObjectOfType<Platform2>();

            _ballSpawner = FindObjectOfType<BallSpawner>();
            _platformSpawner1 = FindObjectOfType<PlatformSpawner1>();
            _platformSpawner2 = FindObjectOfType<PlatformSpawner2>();

            _playerController1 = FindObjectOfType<Player1Controller>();
            _playerController2 = FindObjectOfType<Player2Controller>();
        }

        private void Start()
        {
            _objectsManager = this;
        }

        public void ResetPlatforms()
        {
            ResetPlatform(_platform1, _platformSpawner1);
            ResetPlatform(_platform2, _platformSpawner2);
        }

        public void ResetBall()
        {
            _ball.transform.position = _ballSpawner.transform.position;
            _ball.transform.rotation = _ballSpawner.transform.rotation;
            _ball.Reset();
        }

        public void EnablePlayersControls()
        {
            _playerController1.EnableControlsMap();
            _playerController2.EnableControlsMap();
        }

        public void DisablePlayersControls()
        {
            _playerController1.DisableControlsMap();
            _playerController2.DisableControlsMap();
        }

        public void ResetPlayersInertia()
        {
            _playerController1.ResetInertia();
            _playerController2.ResetInertia();
        }

        public void PauseBallMoving()
        {
            _ball.PauseMove();
        }

        public void UnPauseBallMoving()
        {
            _ball.UnPauseMoving();
        }

        private void ResetPlatform(Platform platform, PlatformSpawner platformSpawner)
        {
            platform.transform.position = platformSpawner.transform.position;
            PlayerControllerBase playerController = null;
            platform.TryGetComponent<PlayerControllerBase>(out playerController);
            playerController?.ResetInertia();
        }

        public static ObjectsManager GetInstance()
        {
            return _objectsManager;
        }
    }
}