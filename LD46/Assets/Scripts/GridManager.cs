using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager gridManager;

    public int gridX;
    public int gridY;
    public int cellSize;
    public int[,] gridArray;
    public PixelPerfectCamera ppc;
    public TileBase testTile;
    public TileBase testWall;
    public Grid gridComponent;
    public Tilemap tilemapComponent;
    public Tilemap tilemapWall;

    private void Awake()
    {
        gridManager = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        ppc = Camera.main.GetComponent<PixelPerfectCamera>();
        gridComponent = GetComponent<Grid>();
        //tilemapComponent = GetComponentInChildren<Tilemap>();
        ppc.refResolutionX = gridX * cellSize;
        ppc.refResolutionY = gridY * cellSize;
        gridArray = new int[gridX,gridY];

    }

    public bool checkCell(Vector2Int dir, Vector2Int pos,Transform rayPos,LayerMask mask)
    {
        Vector3Int newPos = new Vector3Int(pos.x + dir.x, pos.y + dir.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayPos.position, dir,0.5f,mask);

        //if (tilemapWall.GetTile(newPos))
        //{
        //    return false;
        //}
        //else 
        if (hit && (!hit.collider.isTrigger || hit.transform.tag == "Object" || hit.transform.tag == "Player" || (hit.transform.tag == "Cultist" && !hit.transform.GetComponent<PickUpObj>().isPickedUp) || (hit.transform.tag == "BlockCult" && rayPos.tag == "Cultist")))
        {
            Debug.Log(hit.transform.name);
            return false;
        }
        return true;
    }
}
