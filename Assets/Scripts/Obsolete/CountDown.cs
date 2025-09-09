using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 5;
    public bool takingAway = false;

    public void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    public void Update()
    {
       if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }

       if (secondsLeft <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft > 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }

        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        
        takingAway = false;
        
    }
}
