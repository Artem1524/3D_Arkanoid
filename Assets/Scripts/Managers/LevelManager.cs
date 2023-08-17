using System.Collections.Generic;
using UnityEngine;

using Arkanoid.GameObjects;

namespace Arkanoid.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField, Range(1, 5)]
        [Tooltip(" оличество блоков на каждой оси (длина, ширина, высота)")]
        private int _blockCountInDimension = 2; // 3 x 3 x 3 = 27 blocks

        [SerializeField]
        [Tooltip("Prefab дл€ разрушаемого блока (со всеми скриптами)")]
        private Block _blockPrefab;

        private BlocksGroupParent _blockGroupParent;
        private BlockSpawner _blockSpawner;
        private int _currentBlockCount;

        private LinkedList<Block> _blocks = new LinkedList<Block>();
        private int _currentLevelIndex;

        private static LevelManager _levelManager;

        private void Awake()
        {
            _blockGroupParent = FindObjectOfType<BlocksGroupParent>();
            _blockSpawner = FindObjectOfType<BlockSpawner>();
        }

        private void Start()
        {
            _levelManager = this;
        }

        public void StartFirstLevel()
        {
            _currentLevelIndex = 1;

            GenerateLevel();
        }

        public void StartNewLevel()
        {
            _currentLevelIndex++;

            GenerateLevel();
        }

        public void RestartLevel()
        {
            DestroyBlocks();

            if (_currentLevelIndex == 1)
            {
                GenerateLevel();
            }
            else
            {
                GenerateLevel();
            }
        }

        public void BlockDestroyed()
        {
            _currentBlockCount--;

            if (_currentBlockCount <= 0)
            {
                ResetGameObjects();

                DestroyBlocks();

                Debug.LogWarning("”ровень пройден");

                StartNewLevel();
            }
        }

        public void ResetGameObjects()
        {
            ObjectsManager.GetInstance().ResetPlatforms();
            ObjectsManager.GetInstance().ResetBall();
        }

        public void BallExit()
        {
            LivesManager.GetInstance().DecreaseLive();

            if (LivesManager.GetInstance().IsLivesLeft())
            {
                GameManager.GetInstance().FinishGame();
            }

            ResetGameObjects();
        }

        private Block CreateBlock(int index)
        {
            Block block = UnityEngine.Object.Instantiate<Block>(_blockPrefab);

            SetBlockPosition(block, index);
            block.transform.rotation = UnityEngine.Random.rotation;

            block.OnDestroy += () => block.gameObject.SetActive(false);

            block.transform.parent = _blockGroupParent.transform;

            return block;
        }

        /*
         * Example: blockindex = 15
         * 
         *      0  1  2
         *    T -- -- --> x
         *  0 |
         *  1 |
         *  2 | []
         *  y v
         * 
         * X_Index = 0
         * Y_Index = 2 (в Unity ось y это ось z) (ширина)
         * Z_Index = 1 (в Unity ось z это ось y) (высота)
         * 
         * */
        private void SetBlockPosition(Block block, int index)
        {
            Vector3 blockPosition = _blockSpawner.transform.position;

            int blockXIndex = index % _blockCountInDimension;
            int blockYIndex = index / _blockCountInDimension % _blockCountInDimension;
            int blockZIndex = index / _blockCountInDimension / _blockCountInDimension;

            float offset = _blockCountInDimension / 2.0f - 0.5f;

            blockPosition.x = ((blockXIndex - offset) * _blockPrefab.transform.localScale.x * 1.74f) + blockPosition.x;
            blockPosition.z = ((blockYIndex - offset) * _blockPrefab.transform.localScale.z * 1.74f) + blockPosition.z;
            blockPosition.y = ((blockZIndex - offset) * _blockPrefab.transform.localScale.y * 1.74f) + blockPosition.y;

            block.transform.position = blockPosition;
        }

        private void GenerateLevel()
        {
            int blockCount = (int)Mathf.Pow(_blockCountInDimension, 3);
            _currentBlockCount = blockCount;

            for (int index = 0; index < blockCount; index++)
            {
                Block block = CreateBlock(index);
                _blocks.AddLast(block);
            }

            ResetGameObjects();
        }

        private void DestroyBlocks()
        {
            foreach (Block block in _blocks)
            {
                Destroy(block.gameObject);
            }

            _blocks.Clear();
        }

        public static LevelManager GetInstance()
        {
            return _levelManager;
        }
    }
}