using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeTrigger : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip[] eatSounds;
    public AudioClip happy;
    public AudioClip sad;

    private void Start()
    {
        GetComponent<Animator>().speed = 0.3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Cultist")
        {
            GetComponent<Animator>().speed = 1;
            if (collision.tag == "Player")
            {
                AS.PlayOneShot(happy);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Cultist" && collision.GetComponent<PickUpObj>().isPickedUp == false)
        {
            GameManager.manager.Heal(collision.GetComponent<PickUpObj>().sacrificeValue);
            GameManager.manager.numOfCultists--;
            GameManager.manager.cultistText.text = "C : " + GameManager.manager.numOfCultists.ToString();
            AS.PlayOneShot(eatSounds[Random.Range(0, eatSounds.Length)]);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player" || collision.tag == "Cultist")
        {
            GetComponent<Animator>().speed = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Cultist")
        {
            GetComponent<Animator>().speed = 0.3f;
            if (collision.tag == "Player" && AS.isPlaying == false)
            {
                AS.PlayOneShot(sad);
            }

        }
    }
}
