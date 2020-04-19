using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    public SpriteRenderer spriteRender;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public bool canPickUp;
    public bool isPickedUp;
    public Vector3 offset = new Vector3(0.5f,0.5f,0f);
    public bool wanders;
    private float wanderFreq = 1f;
    public LayerMask layerMask;
    public int sacrificeValue;
    public AudioClip[] screams;
    public float SoundDelay = 0.1f;
    private float nextSound;
    public AudioSource AS;
    public void Start()
    {
        if (wanders)
        {
            InvokeRepeating("MoveUnit", Random.Range(1f, 5f), wanderFreq);
        }
    }

    private void Update()
    {
        if(isPickedUp)
        {
            transform.position = Unit.unit.transform.position - offset + Vector3.up;
            if (Time.time > nextSound)
            {
                AS.PlayOneShot(screams[Random.Range(0, screams.Length)]);
                nextSound = Time.time + SoundDelay;
            }
        }
    }

    public void PickUp()
    {
        if (canPickUp)
        {
            CancelInvoke();
            gameObject.layer = 2;
            GetComponent<BoxCollider2D>().isTrigger = true;
            transform.position = Unit.unit.transform.position - offset + Vector3.up;
            GetComponent<Animator>().SetBool("PickedUp", true);
            isPickedUp = true;
        }
    }

    public void PutDown()
    {
        if (isPickedUp)
        {
            gameObject.layer = 0;

            GetComponent<BoxCollider2D>().isTrigger = false;
            transform.position = Unit.unit.transform.position - offset + new Vector3(Unit.unit.lastDir.x, Unit.unit.lastDir.y,0);
            GetComponent<Animator>().SetBool("PickedUp", false);
            isPickedUp = false;
            InvokeRepeating("MoveUnit", 1f, wanderFreq);
        }
    }

    public void MoveUnit()
    {
        if (!isPickedUp)
        {
            Vector2Int dir = new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2));
            //GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + dir*Time.deltaTime);
            Vector2Int oldpos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            if ((dir.x == 0 || dir.y == 0) && GridManager.gridManager.checkCell(dir, oldpos, transform,layerMask))
            {
                transform.Translate(dir.x, dir.y, 0);
                //spriteRender.sprite = ChangeSprite(dir);
                //Debug.Log(ChangeSprite(dir));
            }
            wanderFreq = Random.Range(1f, 5f);
        }
    }

    public Sprite ChangeSprite(Vector2Int dir)
    {
        //Debug.Log(dir);
        if(dir == new Vector2Int(0,1))
        {
            return up;
        }
        else if (dir == new Vector2Int(1, 0))
        {
            return right;
        }
        else if (dir == new Vector2Int(0, -1))
        {
            return down;
        }
        else if (dir == new Vector2Int(-1, 0))
        {
            return left;
        }
        else
        {
            return down;
        }
    }
}
