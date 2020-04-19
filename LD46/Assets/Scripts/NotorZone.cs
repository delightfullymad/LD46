using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotorZone : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cultist")
        {
            GameManager.manager.notor++;
            GameManager.manager.notorText.text = "N : " + GameManager.manager.notor.ToString();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cultist")
        {
            GameManager.manager.notor--;
            GameManager.manager.notorText.text = "N : " + GameManager.manager.notor.ToString();
        }
    }

    
}
