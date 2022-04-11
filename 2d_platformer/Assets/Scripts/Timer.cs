using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //variables to set up the time starting
    public static float FlowingTime;
    public static bool TimerOn = false;
   
   //used to change the text in the TimerTxt under the HUD
    public Text TimerTxt;

    // Start is called before the first frame update
    void Start()
    {
        //Turns timer on
        TimerOn = true;
    }

    //Time is incremented with deltaTime and sent into the UpdateTimer method
    void Update()
    {
        if(TimerOn)
        {
            FlowingTime += (Time.deltaTime / 2);
            UpdateTimer(FlowingTime);
        }
    }

    //Changes the text in TimerTxt to the current time
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        PlayerPrefs.SetString("Minutes", minutes.ToString());
        PlayerPrefs.SetString("Seconds", seconds.ToString());

        //{0} and {1} are used as the parameters inserted into Format() for minutes and seconds
        //and displayed in sets of two numbers
        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}