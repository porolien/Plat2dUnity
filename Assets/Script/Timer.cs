using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text TimerText = null;
    public int TimerDelay = 120;
    private string TimerInString = "";
    public PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int TimerInSeconds = TimerDelay - (int)Time.timeSinceLevelLoad;
        TimerInString = "" + (TimerInSeconds);
        TimerText.text = TimerInString;
        if(TimerInSeconds == 0)
        {
            Player.Die();
        }

    }
}
