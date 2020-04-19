using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Cultist" && collision.GetComponent<PickUpObj>().isPickedUp == false)
        {
            GameManager.manager.Heal(collision.GetComponent<PickUpObj>().sacrificeValue);
            GameManager.manager.numOfCultists--;
            GameManager.manager.cultistText.text = "C : " + GameManager.manager.numOfCultists.ToString();
            Destroy(collision.gameObject);
        }
    }
}
