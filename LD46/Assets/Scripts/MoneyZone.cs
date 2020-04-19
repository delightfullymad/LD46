using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyZone : MonoBehaviour
{
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeMoney", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Cultist")
        {
            num++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cultist")
        {
            num--;
        }
    }

    public void MakeMoney()
    {
        int rand = Random.Range(2, 5);
        GameManager.manager.money += ((num*rand) - GameManager.manager.numOfCultists);
        GameManager.manager.moneyText.text = "$ : " + GameManager.manager.money.ToString();
    }
}
