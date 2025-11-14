using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Camera mainCam;
    [Header("FOR DEBUGGING")]
    [SerializeField] PlayerPawn selectedPlayer;
    int moveRange = 1;
    [SerializeField] GameObject[] validMoveMarkers = new GameObject[9];
    [SerializeField] Team playerTeam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectBoardThings();
        }

        if (Input.GetKey(KeyCode.W))
        {
            mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + 0.01f, mainCam.transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            mainCam.transform.position = new Vector3(mainCam.transform.position.x - 0.01f, mainCam.transform.position.y, mainCam.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 0.01f, mainCam.transform.position.z);
        }
        if(Input.GetKey(KeyCode.D))
        {
            mainCam.transform.position = new Vector3(mainCam.transform.position.x + 0.01f, mainCam.transform.position.y, mainCam.transform.position.z);
        }
    }
    
    void SelectBoardThings()
    {
        //If you don't have enoguh moves
        if (LoginManager.instance.currAccount.numOfMoves <= 0)
            return;


        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

        if (hit.collider != null)
        {
            TerrainTile currTile = hit.collider.GetComponent<TerrainTile>();
            PlayerPawn currPawn = currTile.occupyingPlayer;
            if (selectedPlayer != null && (currTile != null || currPawn.team != playerTeam))///If you select a valid move
            {
                if (math.abs(currTile.terrainPosX - selectedPlayer.posX) <= moveRange && math.abs(currTile.terrainPosY - selectedPlayer.posY) <= moveRange)
                {
                    if (currPawn != null && currPawn.team != playerTeam) //When capturing enemy
                    {
                        Destroy(currPawn.gameObject);
                    }
                    selectedPlayer.MovePlayer(currTile);
                }

                LoginManager.instance.PlayerHasMoved();
                Deselect();
            }
            else if (currPawn != null)///If you select a player
            {
                if (selectedPlayer != null)
                    selectedPlayer.PawnSelected(false);

                selectedPlayer = currPawn;
                currPawn.PawnSelected(true);
                ShowValidMoves(currPawn);
            }
            else //If you hit selected nothing or no valid moves
            {
                Deselect();
            }
        }
    }

    void ShowValidMoves(PlayerPawn currPawn)
    {
        int amountOfMarkers = 0;
        for (int i = -moveRange; i <= moveRange; i++)
        {
            for (int j = -moveRange; j <= moveRange; j++)
            {
                TerrainTile terrainToCheck = TerrainManager.instance.grid[new Vector2(currPawn.currTile.terrainPosX + i, currPawn.currTile.terrainPosY + j)];
                if (terrainToCheck.occupyingPlayer == null || terrainToCheck.occupyingPlayer.team != currPawn.team) //Don't put markers on teammates;
                {
                    validMoveMarkers[amountOfMarkers].SetActive(true);
                    validMoveMarkers[amountOfMarkers].transform.position = terrainToCheck.transform.position;
                    amountOfMarkers++;
                }
            }
        }
    }
    
    void Deselect()
    {
        for (int i = 0; i <= validMoveMarkers.Length - 1; i++) //Hide all markers
        {
            validMoveMarkers[i].SetActive(false);
        }

        if (selectedPlayer != null)
        {
            selectedPlayer.PawnSelected(false);
            selectedPlayer = null;
        }
            
        for(int i = 0; i < 9; i++)
        {
            validMoveMarkers[i].SetActive(false);
        }
    }
}
