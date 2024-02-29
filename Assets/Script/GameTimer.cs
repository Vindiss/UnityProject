using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;
using TMPro;

public class GameTimer : MonoBehaviour
{
    private Timer TimerSeconds;
    public int Timer;

    // Start is called before the first frame update
    void Start()
    {
        TimerSeconds = new Timer(1000);
        TimerSeconds.Elapsed += OnTimedEvent;
        TimerSeconds.Start();
        Timer = 120;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().SetText($"{Timer / 60}:{Timer % 60}");
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        Timer--;
    }
}
