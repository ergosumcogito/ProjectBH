using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")] public Transform player;
    public GameObject enemyPrefab;

    [Header("Spawn Settings")] public float spawnInterval = 0.4f;
    public int maxEnemies = 10;
    public float minSpawnDistance = 5f;
    public float maxSpawnDistance = 15f;

    private float _spawnTimer;
    private bool _isSpawning;

    private readonly List<GameObject> _activeEnemies = new List<GameObject>();

    //automatic start for testing purposes
    private void Start()
    {
        StartSpawning();
    }

    private void Update()
    {
        if (!_isSpawning) return;

        _activeEnemies.RemoveAll(e => e == null);

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= spawnInterval && _activeEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (!player || !enemyPrefab) return;

        Vector2 dir2D = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 pos = player.position + new Vector3(dir2D.x, 0, dir2D.y) * distance;

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        _activeEnemies.Add(enemy);
    }

    //to be used by other systems to control spawning
    public void StartSpawning()
    {
        _isSpawning = true;
        _spawnTimer = 0f;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    public void ClearEnemies()
    {
        foreach (var e in _activeEnemies)
        {
            if (e != null) Destroy(e);
        }

        _activeEnemies.Clear();
    }
}