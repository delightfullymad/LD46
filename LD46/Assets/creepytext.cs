using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class creepytext : MonoBehaviour
{
    public string txt;
    public float textSpeed = 0.1f;
    public Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        if (txt.Length > 0)
        {
            StartCoroutine("ShowDialogueText", txt);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialogue(string txt)
    {
        
        
    }

    IEnumerator ShowDialogueText(string txt)
    {
        if (GameManager.manager.isMainMenu)
        {
            for (int i = 0; i < txt.Length; i++)
            {
                textBox.text = txt.Substring(0, i);

                yield return new WaitForSecondsRealtime(textSpeed);
            }
            textBox.text = txt;
        }
        else StopCoroutine("ShowDialogueText");
    }

    
}
