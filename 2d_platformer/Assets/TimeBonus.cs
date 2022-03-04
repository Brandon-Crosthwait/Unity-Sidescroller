using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    // Start is called before the first frame update

    public static Stopwatch timer = new Stopwatch();  //stopwatch that starts at the beginning of the level

    void Start()
    {
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
