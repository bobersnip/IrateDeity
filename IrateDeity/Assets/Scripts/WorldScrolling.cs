using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    Transform playerTransform;
    Vector2Int currentTilePosition;
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 10f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfViewHorizontal = 3;
    [SerializeField] int fieldOfViewVertical = 3;


    // Awake is called before the first frame update
    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Start()
    {
        UpdateTilesOnScreen();
        playerTransform = GameManager.instance.playerTransform;
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log("Player transform position: " + playerTransform.position);
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x = (playerTilePosition.x - (playerTransform.position.x < 0 ? 1 : 0));
        playerTilePosition.y = (playerTilePosition.y - (playerTransform.position.y < 0 ? 1 : 0));

        if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTilesOnScreen();
        }
    }

    private void UpdateTilesOnScreen()
    {
        for (int pov_x = -(fieldOfViewHorizontal/2); pov_x <= fieldOfViewHorizontal/2; pov_x++)
        {
            for (int pov_y = -(fieldOfViewVertical/2); pov_y <= fieldOfViewVertical/2; pov_y++)
            {
                Debug.Log("Player tile position: " + playerTilePosition);
                Debug.Log("pov: " + pov_x + ", " + pov_y);

                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                
                Debug.Log("Tile to update: " + tileToUpdate_x + ", " + tileToUpdate_y);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                Vector3 newPosition = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
                if (newPosition != tile.transform.position)
                {
                    tile.transform.position = newPosition;
                    tile.GetComponent<TerrainTile>().Spawn();
                }
                
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)(currentValue);
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}
