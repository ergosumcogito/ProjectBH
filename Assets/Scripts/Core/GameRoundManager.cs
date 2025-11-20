using UnityEngine;

public class GameRoundManager : MonoBehaviour
{
    [SerializeField] private LevelEditor levelEditor;
    [SerializeField] private PlayerSpawn playerSpawner;
    [SerializeField] private EnemySpawner enemySpawner;

    private GameObject playerInstance;

    private void OnEnable()
    {
        RoundEvents.OnRoundStart += HandleRoundStart;
        RoundEvents.OnRoundEnd += HandleRoundEnd;
    }

    private void OnDisable()
    {
        RoundEvents.OnRoundStart -= HandleRoundStart;
        RoundEvents.OnRoundEnd -= HandleRoundEnd;
    }

    private void HandleRoundStart(float duration)
    {
        levelEditor.ClearLevel();
        levelEditor.GenerateLevel();

        playerInstance = playerSpawner.SpawnPlayer();

        enemySpawner.ClearEnemies();
        enemySpawner.StartSpawning();
    }

    private void HandleRoundEnd()
    {
        enemySpawner.StopSpawning();
        enemySpawner.ClearEnemies();

        if (playerInstance != null)
            Destroy(playerInstance);
    }
}