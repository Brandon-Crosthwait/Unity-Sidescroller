using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    //Better to reference all Load/Save Scenes in a centralized location. Change the number here
    //if the program's build order changes.
    public enum sceneOrder
    { 
        MainMenu = 0,
        LevelSelect = 1,
        LevelOne = 2,
        LevelOneBoss = 3
    }

}
