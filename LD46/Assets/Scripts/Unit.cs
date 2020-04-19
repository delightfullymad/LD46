using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static Unit unit;

    private float movementAmount;
    public bool canMove = true;
    public bool carrying;
    public Vector2Int lastDir;
    public GameObject currentlyCarrying;
    public LayerMask layerMask;
    public float moveSpeed;
    private float nextMove;
    public AudioClip[] stepSounds;
    // Start is called before the first frame update
    private void Awake()
    {
        unit = this;
    }
    void Start()
    {
        movementAmount = GridManager.gridManager.cellSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveUnit(Vector2Int.right);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveUnit(-Vector2Int.right);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveUnit(Vector2Int.up);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveUnit(Vector2Int.down);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!carrying)
                {
                    PickUp(lastDir);
                }
                else
                {
                    PutDown(lastDir);
                 
                }
            }

        }
    }

    public void MoveUnit(Vector2Int dir)
    {
        if(Time.time < nextMove)
        {
            return;
        }
        nextMove = Time.time + moveSpeed;

        lastDir = dir;
        //GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + dir*Time.deltaTime);
        Vector2Int oldpos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        if (GridManager.gridManager.checkCell(dir, oldpos,transform,layerMask))
        {
            transform.Translate(dir.x,dir.y,0);
            GetComponent<AudioSource>().PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
        }
        
    }

    public void ForceMoveUnit(Vector2Int dir)
    {
        transform.Translate(dir.x, dir.y, 0);
    }

    public void PickUp(Vector2Int dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.5f,layerMask);
        if(hit && hit.transform.GetComponent<PickUpObj>() && hit.transform.GetComponent<PickUpObj>().canPickUp)
        {
            hit.transform.GetComponent<PickUpObj>().PickUp();
            currentlyCarrying = hit.transform.gameObject;
            carrying = true;
            
        }
    }

    public void PutDown(Vector2Int dir)
    {
        Vector2Int oldpos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        if (GridManager.gridManager.checkCell(dir, oldpos, transform,layerMask))
        {

        currentlyCarrying.GetComponent<PickUpObj>().PutDown();
        currentlyCarrying = null;
        carrying = false;
        }
    }

}
