using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private InputReader inputReader;

    
    void Start()
    {
        GameObject playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        PlayerMovement movement = playerInstance.GetComponent<PlayerMovement>();
        movement.setInputReader(inputReader);
    }
}
