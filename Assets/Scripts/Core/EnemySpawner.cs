using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] public GameObject enemyPrefab;

    [Header("Spawn Settings")] public int maxEnemies = 15;
    public float spawnInterval = 0.5f;

    [Tooltip("Spawn at least x tiles away from player")]
    public float minSpawnDistance = 3f;

    //TODO: get size of map dynamically
    private const float LevelWidth = 13 - 1;
    private const float LevelHeight = 13 - 1;

    private float _spawnTimer;
    private bool _isSpawning;

    private readonly List<GameObject> _activeEnemies = new();

    //automatic start; for testing purposes, only temporary
    private void Start()
    {
        StartSpawning();
    }

    private void Update()
    {
        if (!_isSpawning) return;

        _activeEnemies.RemoveAll(e => e == null);

        _spawnTimer += Time.deltaTime;

        if (!(_spawnTimer >= spawnInterval) || _activeEnemies.Count >= maxEnemies) return;
        SpawnEnemy();
        _spawnTimer = 0f;
    }

    private void SpawnEnemy()
    {
        if (!enemyPrefab) return;
        var player = GameObject.FindWithTag("Player")?.transform;
        if (player == null) return;

        var spawnPos = GetSpawnPoint(player.position);

        var enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        _activeEnemies.Add(enemy);
    }

    //creates a square around player that prevents enemies from spawning within, returns enemy spawn point
    private Vector2 GetSpawnPoint(Vector2 playerPos)
    {
        for (var i = 0; i < 50; i++)
        {
            var randomEnemySpawnPoint = GetRandomSpawnPoint();

            if (!IsInSafeZone(playerPos, randomEnemySpawnPoint)) return randomEnemySpawnPoint;
        }

        //fallback if no valid spawn is found
        return new Vector2(0, 0);
    }

    private bool IsInSafeZone(Vector2 playerPos, Vector2 enemySpawnPos)
    {
        return (Mathf.Abs(playerPos.x - enemySpawnPos.x) < minSpawnDistance &&
                Mathf.Abs(playerPos.y - enemySpawnPos.y) < minSpawnDistance);
    }

    //for enemy spawn points
    private static Vector2 GetRandomSpawnPoint()
    {
        var x = Random.Range(0f, LevelWidth);
        var y = Random.Range(0f, LevelHeight);

        return new Vector2(x, y);
    }

    //these three are to be used by other systems to control spawning
    //starts enemy spawning
    public void StartSpawning()
    {
        _isSpawning = true;
        _spawnTimer = 0f;
    }

    //stops enemy spawning
    public void StopSpawning()
    {
        _isSpawning = false;
    }

    //clears all enemies, once time is up for example
    public void ClearEnemies()
    {
        foreach (var e in _activeEnemies.Where(e => e != null))
        {
            Destroy(e);
        }

        _activeEnemies.Clear();
    }
}