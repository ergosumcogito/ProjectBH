using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 10;
    [SerializeField] private int length = 10;
    [SerializeField] private float tileSize = 1f;


    [Header("Tile Prefab")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject borderPrefab;


    [Header("Generation Options")]
    [SerializeField] private bool generateOnStart = true;


    private void Start()
    {
        if (generateOnStart)
            GenerateLevel();
    }

    public void GenerateLevel()
    {
        //Check if TilePrefab or BorderPrefab equals null
        if (tilePrefab == null || borderPrefab == null)
        {
            Debug.Log("TilePrefab or BorderPrefab is null");
            return;
        }
        else
        {
            GenerateTiles();
            Debug.Log("Tiles generated");
            return;
        } 
    }

    //Generates Tiles
    public void GenerateTiles()
    {
        //Generates Tiles incl Border
        int borderX = width + 1;
        int borderY = length + 1;

        for (int x = -1; x < borderX; x++)
        {
            for (int y = -1; y < borderY; y++)
            {
                if (x == -1 || x == width || y == -1 || y == length)
                {
                    Vector3 pos = new Vector3(x * tileSize, y * tileSize, 0);
                    GameObject tile = Instantiate(borderPrefab, pos, Quaternion.identity);
                    tile.name = $"Border x={x}, y={y}";
                }
                else
                {
                    Vector3 pos = new Vector3(x * tileSize, y * tileSize, 0);
                    GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                    tile.name = $"Tile x={x}, y={y}";
                }
            }
        }
    }
}
