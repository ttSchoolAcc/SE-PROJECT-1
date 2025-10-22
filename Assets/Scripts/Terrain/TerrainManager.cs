using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Dictionary<Vector2, TerrainTile> grid = new Dictionary<Vector2, TerrainTile>();
    [SerializeField] Transform gridParent;
    [SerializeField] GameObject terrainGameObj;
    [Space(10)]
    [SerializeField] int startingMapSizeX = 50;
    [SerializeField] int startingMapSizeY = 50;
    [SerializeField] int startingMapPointX = -50;
    [SerializeField] int startingMapPointY = -50;
    [SerializeField] float tileSpacing = 1;
    public static TerrainManager instance;
    [SerializeField] GameObject[] houseStructures;

    [Space(20)]
    [SerializeField] int numPlayerSpawns = 3;
    [SerializeField] PlayerPawn playerPawn;


    void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        for (int i = 0; i < startingMapSizeY; i++) //Have to put this loop in Awake since other scripts need these tiles on Star()
        {
            for (int j = 0; j < startingMapSizeX; j++)
            {
                GameObject newTerrainTile = Instantiate(terrainGameObj, gridParent);
                newTerrainTile.transform.position = new Vector3(startingMapPointX + (i * tileSpacing), startingMapPointY + (j * tileSpacing));
                TerrainTile terrainTileClass = newTerrainTile.GetComponent<TerrainTile>();
                terrainTileClass.terrainPosX = i;
                terrainTileClass.terrainPosY = j;

                grid.Add(new Vector2(i, j), terrainTileClass);
            }
        }
    }


    void Start()
    {
        for (int i = 0; i < numPlayerSpawns; i++)
        {
            for (int j = 0; j < numPlayerSpawns; j++)
            {
                PlayerPawn newPlayer = Instantiate(playerPawn);
                newPlayer.InitializePawn(Team.Blue);
                newPlayer.MovePlayer(grid[new Vector2(20 + i, 25 + j)]);
                //grid[new Vector2(20 + i, 25 + j)].UpdateTile(newPlayer);
            }
        }
        
        for(int i = 0; i < numPlayerSpawns; i++)
        {
            for(int j = 0; j < numPlayerSpawns; j++)
            {
                PlayerPawn newPlayer = Instantiate(playerPawn);
                newPlayer.InitializePawn(Team.Red);
                newPlayer.MovePlayer(grid[new Vector2(30 + i, 25 + j)]);
                //grid[new Vector2(30 + i, 25 + j)].UpdateTile(newPlayer);
            }
        }
    }
}
