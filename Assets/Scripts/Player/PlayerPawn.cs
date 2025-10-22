using Unity.VisualScripting;
using UnityEngine;

public enum Team{Unassigned, Red, Blue}

public class PlayerPawn : MonoBehaviour
{
    [SerializeField] GameObject selectionIndicator;
    public TerrainTile currTile;

    [SerializeField] SpriteRenderer playerSprite;
    public Team team;

    public int posX;
    public int posY;

    public void InitializePawn(Team newTeam)
    {
        team = newTeam;
        if (newTeam == Team.Red)
        {
            playerSprite.color = Color.red;
        }
        else if (newTeam == Team.Blue)
        {
            playerSprite.color = Color.blue;
        }
    }

    public void MovePlayer(TerrainTile newTile)
    {
        if (currTile != null) //This is the previous tile
            currTile.occupyingPlayer = null;

        currTile = newTile;
        currTile.occupyingPlayer = this;
        transform.parent = currTile.transform;
        transform.localPosition = new Vector3(0, 0, 0);

        posX = newTile.terrainPosX;
        posY = newTile.terrainPosY;
    }

    public void PawnSelected(bool selected)
    {
        selectionIndicator.SetActive(selected);
    }
}