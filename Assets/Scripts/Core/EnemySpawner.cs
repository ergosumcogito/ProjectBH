using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] public GameObject enemyPrefab;

    [Header("Spawn Settings")] public int maxEnemies = 15;
    public float spawnInterval = 0.5f;

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


    //TODO: implement some kinda spawn distance, so enemies don't spawn too close to the player
    private void SpawnEnemy()
    {
        if (!enemyPrefab) return;

        var x = Random.Range(0, LevelWidth);
        var y = Random.Range(0, LevelHeight);

        var pos = new Vector3(x, y, 0f);

        var enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        _activeEnemies.Add(enemy);
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