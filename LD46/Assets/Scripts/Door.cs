using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3Int dir;
    public GameObject tutorialDelete;
    public bool updateGod;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Debug.Log("DOOR");
            collision.GetComponent<Unit>().ForceMoveUnit(new Vector2Int(dir.x*2,dir.y*2));
            Camera.main.GetComponent<CameraController>().StartCoroutine("ChangeRoom", (Vector3)dir);
            if (tutorialDelete)
            {
                Destroy(tutorialDelete);
            }
            if(updateGod)
            {
                GameManager.manager.GodObj.GetComponent<Animator>().SetInteger("Size", GameManager.manager.godSize);
            }
        }
    }
}
