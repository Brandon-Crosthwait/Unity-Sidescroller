using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Sound : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource source;
    
 
    /// <summary>
    /// used to play the boss attack sound
    /// </summary>
    private void OnEnable()
    {
        source.Play();
    }
}
