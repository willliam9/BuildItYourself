using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TimerManager : MonoBehaviour
{
    [Header("Component")]
   public TextMeshProUGUI timerText;

    private GameManager gameManager;

    void Start()
    {
        gameManager =  FindObjectOfType<GameManager>();
    }


    void Update()
    {

        TimeSpan timeSpan = TimeSpan.FromSeconds(gameManager.elapsedTime);
        timerText.text = timeSpan.ToString(@"hh\:mm\:ss");

    }
}
