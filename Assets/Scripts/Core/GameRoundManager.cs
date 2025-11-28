using UnityEngine;

// --------------------------
// --- Game Round Manager ---
// --------------------------
// Handles gameplay: spawning, etc.
// Clears objets (player, enemies)
//
// Basically it does things on the command of round system
// (RoundSystem - state, GameRoundManager - execute actions)
//
// ? (possibly moved to other class) Assigns weapon to player via Weapon Factory


public class GameRoundManager : MonoBehaviour
{
    [SerializeField] private LevelEditor levelEditor;
    [SerializeField] private PlayerSpawn playerSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    
    // TODO testing weapons
    [SerializeField] private WeaponFactory weaponFactory;


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
        
        // -----------------------------
        // TEST: give player a pistol
        // -----------------------------
        weaponFactory.weaponSlot = playerInstance.transform.Find("WeaponSlot");
        weaponFactory.CreateWeapon("Pistol");
        // -----------------------------

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