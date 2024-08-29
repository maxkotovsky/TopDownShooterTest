using UnityEngine;
using Gamecore;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameDataSO _gameDataSO;

    [SerializeField]
    private Transform _canvas;

    [SerializeField]
    private MainUI _mainUI;

    [SerializeField]
    private GameOverUI _looseUI;

    [SerializeField]
    private GameOverUI _winUI;

    [SerializeField]
    private PlayerUnit _player;
    [SerializeField]
    private GameObject _playerSpawnPosition;

    [SerializeField]
    private GameObject[] _enemySpawnPositions;
    [SerializeField]
    private EnemyUnitFactory _enemyUnitFactory;

    [SerializeField] private FinishLineListener _finishLineListener;

    private GameData gameData;
    private Coroutine _spawnEnemiesCoroutine;
    private int _enemyToKill;

    void Start()
    {
        gameData = _gameDataSO.gameData;

        _spawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
        SpawnPlayer();
        _enemyToKill = GetRandomWinCondition(gameData);
    }

    public int GetRandomWinCondition(GameData data)
    {
        return Random.Range(data.minEnemyKillToWin, data.maxEnemyKillToWin);
    }

    private void SpawnPlayer()
    {
        if (_player != null && _playerSpawnPosition != null)
        {
            Vector3 playerSpawnPosition = _playerSpawnPosition.transform.position;
            PlayerUnit player = Instantiate(_player, playerSpawnPosition, Quaternion.identity);
            player.Initialize(gameData);
            _mainUI.SetPlayerUnit(player);
            player.OnPlayerDeath.AddListener(GameOver);
            _finishLineListener.OnEnemyHit.AddListener(player.OnEnemyHitFinish);
        }
        else
        {
            Debug.LogError("Player or PlayerSpawnPosition is not assigned in GameManager!");
        }
    }

    private System.Collections.IEnumerator SpawnEnemies()
    {
        SpawnEnemy();

        while (true)
        {
            float waitTime = _enemyUnitFactory.GetRandomSpawnTime(gameData);
            yield return new WaitForSeconds(waitTime);

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyUnitFactory != null)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            BaseUnit baseUnit = _enemyUnitFactory.CreateUnit(gameData, spawnPosition, Quaternion.identity);
            EnemyUnit enemy = baseUnit as EnemyUnit;

            if (enemy != null)
            {
                enemy.OnEnemyDeathOnKill.AddListener(CountEnemyDeath);
            }
        }
        else
        {
            Debug.LogError("EnemyFactory is not assigned in GameManager!");
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, _enemySpawnPositions.Length);
        return _enemySpawnPositions[randomIndex].transform.position;
    }

    private void CountEnemyDeath()
    {
        _enemyToKill--;
        Debug.Log($"EnemyToKill {_enemyToKill}");
        WinConditionsCheck();
    }

    private void WinConditionsCheck()
    {
        int enemyToKIll = _enemyToKill;
        if (enemyToKIll <= 0)
        {
            GameOver();
        }
    }

    public void DestroyAllEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(Vector2.zero, 200f, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject, 0.5f);
        }
    }

    public void GameOver()
    {
        StopCoroutine(_spawnEnemiesCoroutine);
        DestroyAllEnemies();

        if (_enemyToKill == 0)
        {
            Instantiate(_winUI, _canvas);
        }
        else
        {
            Instantiate(_looseUI, _canvas);
        }

    }
}
