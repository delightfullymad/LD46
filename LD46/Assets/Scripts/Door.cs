using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3Int dir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
        Debug.Log("DOOR");
            collision.GetComponent<Unit>().MoveUnit(new Vector2Int(dir.x*2,dir.y*2));
            Camera.main.GetComponent<CameraController>().StartCoroutine("ChangeRoom", (Vector3)dir);
        }
    }
}
