using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using GestureRecognizer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyManager
{
    public class EnemyManager : MonoBehaviour
    {
        public List<GameObject> enemies = new List<GameObject>();
        public DifficultyLevel difficultyLevel;

        private List<GesturePattern> _gesturePatterns;

        // Screen info
        private float _minX, _maxX, _maxY;

        # region EnemyInfo

        private Vector3 _enemySize;
        private const float EnemyMargin = 0.1f;
        private IDifficulty _difficulty;

        private float _columnSize;
        private int _columnCount;

        # endregion

        private const int UpdateToMedium = 20;
        private const int UpdateToHard = 50;

        private int _killCount;

        public void UpdateDifficulty()
        {
            _killCount++;

            switch (_killCount)
            {
                case UpdateToMedium:
                    _difficulty = DifficultyBuilder.Build(DifficultyLevel.Medium);
                    break;
                case UpdateToHard:
                    _difficulty = DifficultyBuilder.Build(DifficultyLevel.Hard);
                    break;
            }
        }

        private void Start()
        {
            SetupGesturePatterns();
            SetupScreenSize();
            SetupEnemyInfo();
            SetupDifficultyLevel();

            StartCoroutine(nameof(Spawn));
        }

        private void SetupGesturePatterns()
        {
            var recognizer = GameObject.FindWithTag("Recognizer").GetComponent<Recognizer>();
            _gesturePatterns = recognizer.patterns;
        }

        private void SetupScreenSize()
        {
            var orthographicSize = Camera.main.orthographicSize;
            var screenAspect = Screen.width / (float) Screen.height;

            var cameraPosition = Camera.main.transform.position;
            _minX = cameraPosition.x - orthographicSize * screenAspect;
            _maxX = cameraPosition.x + orthographicSize * screenAspect;

            _maxY = cameraPosition.y + orthographicSize;
        }

        private void SetupEnemyInfo()
        {
            var sampleEnemy = enemies[0];
            _enemySize = sampleEnemy.GetComponent<BoxCollider2D>().size;

            _columnCount = (int) ((_maxX - _minX) / (_enemySize.x + EnemyMargin));
            _columnSize = (_maxX - _minX) / _columnCount;
        }

        private void SetupDifficultyLevel()
        {
            _difficulty = DifficultyBuilder.Build(difficultyLevel);
        }

        private void SpawnEnemyAtColumn(GameObject enemy, List<GesturePattern> patterns, int column)
        {
            var x = _minX + (column * _columnSize) + _columnSize / 2f;
            var y = _maxY + _enemySize.y;

            var enemyPosition = new Vector3(x, y, 0f);

            var instantiated = Instantiate(enemy, enemyPosition, Quaternion.identity);

            var newEnemy = instantiated.GetComponent<Enemy.Enemy>();
            newEnemy.SetGesturePatterns(patterns);

            var newEnemyController = instantiated.GetComponent<Enemy2DController>();
            newEnemyController.speed *= _difficulty.EnemySpeedMultiplier();
        }

        private IEnumerable<int> SpawnColumns(int count, int min, int max)
        {
            var candidates = new HashSet<int>();

            for (var top = max - count; top < max; top++)
            {
                if (!candidates.Add(Random.Range(min, top + 1)))
                {
                    candidates.Add(top);
                }
            }

            var result = candidates.ToList();

            for (var i = result.Count - 1; i > 0; i--)
            {
                var k = Random.Range(0, i + 1);
                var tmp = result[k];
                result[k] = result[i];
                result[i] = tmp;
            }

            return result;
        }


        private IEnumerator Spawn()
        {
            while (true)
            {
                var enemyCount = Random.Range(_difficulty.MinEnemyCount(), _difficulty.MaxEnemyCount() + 1);
                var spawnColumns = SpawnColumns(enemyCount, 0, _columnCount);

                foreach (var column in spawnColumns)
                {
                    var index = Random.Range(0, enemies.Count);
                    var patterns = GetRandomPatterns();
                    SpawnEnemyAtColumn(enemies[index], patterns, column);
                }

                var nextSpawn = Random.Range(_difficulty.MinSpawnSeconds(), _difficulty.MaxSpawnSeconds());
                yield return new WaitForSeconds(nextSpawn);
            }
        }

        private List<GesturePattern> GetRandomPatterns()
        {
            var numPatterns = Random.Range(_difficulty.MinGesturePatterns(), _difficulty.MaxGesturePatterns() + 1);
            var patterns = new List<GesturePattern>();

            for (var i = 0; i < numPatterns; i++)
            {
                var randomPattern = _gesturePatterns[Random.Range(0, _gesturePatterns.Count)];
                patterns.Add(randomPattern);
            }

            return patterns;
        }
    }
}