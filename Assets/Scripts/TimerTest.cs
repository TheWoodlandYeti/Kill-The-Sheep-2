using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerTest : MonoBehaviour
{
    public Text timerText;
    public TextMeshProUGUI sheepCountText;
    public int sheepCount;
    public float theTime = 15;
    [NonSerialized]
    private int childCount;
    private GameObject sheepParent;

    void Start()
    {
        sheepParent = GameObject.FindGameObjectWithTag("SheepParent");
    }


    // Update is called once per frame
    void Update()
    {
        CalculateTime();
        DisplaySheepCount();
    }

    public void CalculateTime()
    {
        theTime -= Time.deltaTime;
        string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
        string seconds = (theTime % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;

        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            FindFirstObjectByType<GameManager>().WinGame();
        }

        if (theTime <= 0)
        {
            FindFirstObjectByType<GameManager>().EndGame();
        }
    }

    public void DisplaySheepCount()
    {
        if (sheepParent != null)
        {
            sheepCount = sheepParent.transform.childCount;
            sheepCountText.text = "Sheep Left: " + sheepCount.ToString();
        }
        else
        {
            Debug.Log("No Sheep Parent!");
        }
    }
}
