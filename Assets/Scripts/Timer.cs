using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    int time = 0;

    private void Start()
    {
        InvokeRepeating("IncrementTime", 0f, 1f);
    }

    void IncrementTime()
    {
        /* time += 1;
        timerText.text = time.ToString("00:00"); */

        time += 1;
        int minutes = time / 60;
        int seconds = time % 60;
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}

