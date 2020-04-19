using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInstance : MonoBehaviour
{
    public int ID;
    public Quest quest;

    public Text qName;
    public Text qDescription;
    public Image qImage;
    public bool completed;
    public Sprite[] icons;
    // Start is called before the first frame update
    void Start()
    {
        
        if(quest == null)
        {
            quest = QuestGenerator();
            GameManager.manager.quests[ID] = transform;
        }
            qName.text = quest.questName;
            qDescription.text = quest.questDescription;
            qImage.sprite = quest.questImage;
    }

    public void Complete()
    {
        completed = true;
        qName.color = Color.gray;
        qDescription.color = Color.gray;
        qImage.color = Color.gray;
        GetComponent<Image>().color = Color.gray;
        GameManager.manager.ShowMessage(qName.text + " COMPLETED");
    }

    Quest QuestGenerator()
    {
        int res = Random.Range(0, 4); //Money, Notoriety, Cultists, Time 
        int amount = 0;
        
        switch (res)
        {
            case 0:
                amount = Random.Range(1000, 2000);
                break;
            case 1:
                amount = Random.Range(10, 25);
                break;
            case 2:
                amount = Random.Range(25, 50);
                break;
            case 3:
                amount = Random.Range(200, 500);
                break;

            default:
                break;
        }
        return new Quest((Resource)res, amount, false, icons[res]);
    }
}
