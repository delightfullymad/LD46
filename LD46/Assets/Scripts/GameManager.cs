using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public CanvasGroup tradeMenu;
    private bool tradeMenuOpen = false;

    public CanvasGroup gameOverWindow;
    private bool gameOverOpen = false;
    public Text gameOverText1;
    public Text gameOverText2;
    public Text glitchText;
    public CanvasGroup quitMenu;
    private bool quitMenuOpen = false;

    public CanvasGroup messageWindow;
    public Text messageText;
    public Transform[] quests = new Transform[3];
    public float decaySpeed = 10f;
    public float spawnTimer = 10f;

    public CanvasGroup winWindow;
    public bool winWindowOpen;
    public bool gameWon;

    public CanvasGroup mainMenu;
    public bool isMainMenu = true;

    public int godSize = 1;
    public GameObject GodObj;
    public CanvasGroup glitchText2;
    public float targetAlpha;
    private bool isPaused = false;

    public AudioClip notificationSound;
    public AudioClip tickSound;
    public AudioSource AS;


    public void Awake()
    {
        manager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "$ : " + money.ToString();
        cultistText.text = "C : "+numOfCultists.ToString();
        notorText.text = "N : " + notor.ToString();
        maxHealth = godHealth;
        InvokeRepeating("DropHealth", decaySpeed, decaySpeed);
        InvokeRepeating("IncreaseTimer", 1f, 1f);
        InvokeRepeating("SpawnCultist", spawnTimer, spawnTimer);
        Time.timeScale = 0;
        isPaused = true;
        isMainMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMainMenu)
        {
            if(Input.anyKeyDown)
            {
                mainMenu.alpha = 0;
                Time.timeScale = 1;
                isMainMenu = false;
                isPaused = false;
                Destroy(mainMenu.gameObject);
            }
        }


        if(glitchText2.alpha < targetAlpha)
        {
            glitchText2.alpha += 0.01f*Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            toggleQuestMenu();
        }

        if(money < 0 && notor >= 10)
        {
            //popup asking to trade notoriety
            OpenTrade();
        }
        else if(money < 0 && notor < 10)
        {
            GameOver(1);
        }
        if (godHealth <= 0)
        {
            GameOver(0);
        }

        if (tradeMenuOpen == true)
        {
            if(Input.GetKeyDown(KeyCode.Y)){
                money += 100;
                notor -= 10;
                moneyText.text = "$ : " + money.ToString();
                notorText.text = "N : " + notor.ToString();
                CloseTrade();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                GameOver(2);
            }
        }

        if(gameOverOpen)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                isPaused = false;
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                isPaused = false;
                Time.timeScale = 1;
                Application.Quit();
            }
        }

        if (quitMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                CloseQuitMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            
            if (quitMenuOpen)
            {
                CloseQuitMenu();
            }
            else { OpenQuitMenu(); }
        }
        if (winWindowOpen)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                CloseWinMenu();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                Application.Quit();
            }
        }
    }

    public void OpenWinMenu()
    {
        isPaused = true;
        winWindow.alpha = 1;
        winWindowOpen = true;
        gameWon = true;
        Time.timeScale = 0;
    }

    public void CloseWinMenu()
    {
        isPaused = false;
        winWindow.alpha = 0;
        winWindowOpen = false;
        Time.timeScale = 1;

    }

    public void OpenQuitMenu()
    {
        isPaused = true;
        quitMenu.alpha = 1;
        quitMenuOpen = true;
        Time.timeScale = 0;
        

    }
    public void CloseQuitMenu()
    {
        isPaused = false;
        quitMenu.alpha = 0;
        quitMenuOpen = false;
        Time.timeScale = 1;
        
    }

    public void GameOver(int ending)
    {
        isPaused = true;
        Time.timeScale = 0;
        gameOverWindow.alpha = 1;
        gameOverOpen = true;
        switch (ending) //0 = god, 1= poor, 2= decline
        {
            case 0:
                gameOverText1.text = "Y̸͎̋O̸̬͛U̷̝̕ ̵͔̆H̸̨̅A̶͕͌V̵̡̾E̷̠͗ ̵̺́F̴̻͘Ă̸̱I̵͍͋L̴̮̈́Ḙ̴̄D̵̺̕ ̵̯̈́Ṃ̵͠Ḛ̶̚";
                gameOverText2.text = "Ị̶͋F̷̡̒ ̶͔̌Y̷̫͆Ŏ̴̢U̸͓͑ ̸̝͠C̵̺͌Ȁ̵̜N̸̡͒N̵͇͌O̷̡̊T̸̢͝ ̷̈͜P̴̉͜R̷̲̈́Ò̸̙Ṿ̵́I̵͇̋D̴̦̉É̷̥ ̷͖͆M̶̖̉Ẹ̵̽ ̷̤͌W̵͎̊Ï̸̠Ṭ̷̉H̸͚̊ ̸̝́S̷̫͠U̷̜̔S̴̤̓Ť̴͔E̵͇̓N̶̬̆Ā̵̞N̸̳͂C̴̯̈́E̵̡͠ ̵̠͝I̶͔͑ ̵͘ͅȘ̸͒H̴̨̀A̷͎̐L̵̡͐L̴̅͜ ̴̮͒H̴̲̾A̴͖͠V̸͕̎E̷̜̐ ̸̺̊T̶̞̓O̷̼̐ ̴̨͌S̴̝͛U̶̱̇S̷̮̋T̸͚̈́Å̴͇I̵̝̓N̶̨̋ ̸̰͝W̶̜̾I̷̝̊T̵̻̚H̸̲͒Ḭ̸͝N̴͙͝ ̷̤͗Y̷͚̊O̷̼̾Ú̴̠ ̶̤̍I̸̭͊N̶̳͠Ş̸̚T̶̠͒Ê̷̤A̸͒͜D̴̗͗.̷̺́.̷̱̑.̸̠̒";
                break;
            case 1:
                gameOverText1.text = "OUT OF MONEY";
                gameOverText2.text = "Your cult ran out of money and didn't have enought notoriety to call in some favors.";
                break;
            case 2:
                gameOverText1.text = "ESCAPE";
                gameOverText2.text = "Your cult ran out of money so you made a break for it. But you WILL be found...eventually.";

                break;
            default:
                break;
        }

    }

    public void OpenTrade()
    {
        isPaused = true;
        tradeMenu.alpha = 1;
        tradeMenuOpen = true;
        Time.timeScale = 0;
    }

    public void CloseTrade()
    {
        isPaused = false;
        tradeMenu.alpha = 0;
        tradeMenuOpen = false;
        Time.timeScale = 1;
    }

    public void ShowMessage(string message)
    {
        CancelInvoke("HideMessage");
        AS.PlayOneShot(notificationSound);
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
            isPaused = false;
            Unit.unit.canMove = true;
            questMenuOpen = false;
            Time.timeScale = 1;
            return;
        }
        else if (!isPaused)
        {
            questMenu.alpha = 1;
            isPaused = true;

            Unit.unit.canMove = false;
            questMenuOpen = true;
            Time.timeScale = 0;
        }
    }

    void DropHealth()
    {
        godHealth--;
        AS.PlayOneShot(tickSound);
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
        targetAlpha += 0.01f;
        decaySpeed = Mathf.Clamp(decaySpeed, 0.5f, 10f);
        godSize++;
       //GodObj.GetComponent<Animator>().SetInteger("Size", godSize);

        InvokeRepeating("DropHealth", decaySpeed, decaySpeed);

    }

    void SpawnCultist()
    {
        //Instantiate(cultistPrefab, pos, Quaternion.identity);
        //numOfCultists++;
        int tempNum = 0;
        int tempMun = 0;
        for (int i = 0; i < notor; i++)
        {
            if (Random.value < 0.5f)
            {
                Vector3 pos = cultSpawner.position + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0);
                Instantiate(cultistPrefab, pos, Quaternion.identity);
                cultistText.text = "C : " + numOfCultists.ToString();
                int muns = Random.Range(0, 50);
                tempNum++;
                tempMun += muns;
            }
        }
        if(tempNum > 0)
        {
            numOfCultists += tempNum;
            money += tempMun;
            ShowMessage("Gained "+tempNum+" new cultist(s) bringing $" + tempMun.ToString());
        }
        spawnTimer = Random.Range(10f, 30f);
        
    }

    public void CheckQuests()
    {
        if (gameWon) return;

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
        if(quests[0].GetComponent<QuestInstance>().completed && quests[1].GetComponent<QuestInstance>().completed && quests[2].GetComponent<QuestInstance>().completed)
        {
            OpenWinMenu();
        }
    }

}
