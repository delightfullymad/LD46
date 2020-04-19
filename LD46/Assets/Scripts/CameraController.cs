using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeRoom(Vector3 pos)
    {
        Vector3 pos1 = transform.position;
        pos = transform.position + new Vector3(pos.x * GridManager.gridManager.gridX, pos.y * GridManager.gridManager.gridY,0);
        for (float t = 0f; t < 1f; t += Time.deltaTime)
        {
            FindObjectOfType<Unit>().canMove = false;
            transform.position = Vector3.Lerp(pos1, pos, t / 1f);
            yield return 0;
        }
        transform.position = pos;
        FindObjectOfType<Unit>().canMove = true;

    }
}
