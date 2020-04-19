using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int godHealth = 64;
    private int maxHealth;
    public Slider healthBar;
    public int money;
    public int notor;
    public Text moneyText;
    public Text notorText;
    public Transform cultSpawner;
    public GameObject cultistPrefab;
    public int numOfCultists;
    public Text cultistText;
    public Text timeText;
    public int timeSurvived;
    public CanvasGroup questMenu;
    private bool questMenuOpen = false;

    public CanvasGroup messageWindow;
    public Text messageText;
    public Transform[] quests = new Transform[3];
    public float decaySpeed = 10f;

    public void Awake()
    {
        manager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cultistText.text = "C : "+numOfCultists.ToString();
        notorText.text = "N : " + notor.ToString();
        maxHealth = godHealth;
        InvokeRepeating("DropHealth", decaySpeed, decaySpeed);
        InvokeRepeating("IncreaseTimer", 1f, 1f);
        InvokeRepeating("SpawnCultist", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            toggleQuestMenu();
        }

    }

    public void ShowMessage(string message)
    {
        CancelInvoke("HideMessage");
        messageWindow.alpha = 0;
        messageText.text = message;
        messageWindow.alpha = 1;
        Invoke("HideMessage", 5f);
    }

    void HideMessage()
    {
        messageWindow.alpha = 0;
    }

    void toggleQuestMenu()
    {
        if (questMenuOpen)
        {
            questMenu.alpha = 0;
            Unit.unit.canMove = true;
            questMenuOpen = false;
            Time.timeScale = 1;
            return;
        }
        else
        {
            questMenu.alpha = 1;
            Unit.unit.canMove = false;
            questMenuOpen = true;
            Time.timeScale = 0;
        }
    }

    void DropHealth()
    {
        godHealth--;
        healthBar.value = godHealth;
    }

    public void IncreaseTimer()
    {
        timeSurvived++;
        timeText.text = "T : " + timeSurvived.ToString();
        CheckQuests();
    }

    public void Heal(int amount)
    {
        CancelInvoke("DropHealth");
        godHealth += amount;
        if(godHealth>maxHealth)
        {
            godHealth = maxHealth;
        }
        healthBar.value = godHealth;

        decaySpeed -= 0.5f;
        decaySpeed = Mathf.Clamp(decaySpeed, 0.1f, 10f);

        InvokeRepeating("DropHealth", decaySpeed, decaySpeed);

    }

    void SpawnCultist()
    {
        //Instantiate(cultistPrefab, pos, Quaternion.identity);
        //numOfCultists++;
        for (int i = 0; i < notor; i++)
        {
            if (Random.value < 0.5f)
            {
                Vector3 pos = cultSpawner.position + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0);
                Instantiate(cultistPrefab, pos, Quaternion.identity);
                numOfCultists++;
                cultistText.text = "C : " + numOfCultists.ToString();
                int muns = Random.Range(0, 100);
                money += muns;
                ShowMessage("Gained new cultist bringing $" + muns.ToString());
            }
        }
        
    }

    public void CheckQuests()
    {
        foreach (Transform t in quests)
        {
            
            QuestInstance qI = t.GetComponent<QuestInstance>();
            if(qI.completed)
            {
                return;
            }
            switch (qI.quest.res)
            {
                case Resource.Money:
                    if(money >= qI.quest.amount)
                    {
                        qI.quest.completed = true;
                        qI.Complete();
                    }
                    break;
                case Resource.Notoriety:
                    if (notor >= qI.quest.amount)
                    {
                        qI.quest.completed = true;
                        qI.Complete();
                    }
                    break;
                case Resource.Cultists:
                    if (numOfCultists >= qI.quest.amount)
                    {
                        qI.quest.completed = true;
                        qI.Complete();
                    }
                    break;
                case Resource.Time:
                    if (timeSurvived >= qI.quest.amount)
                    {
                        qI.quest.completed = true;
                        qI.Complete();
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
