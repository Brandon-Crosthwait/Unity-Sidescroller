using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlipScript : MonoBehaviour
{
    public AIPath aiPath;   //AIPath script reference

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)     //if his x movement is positive, face right
        {
            transform.localScale = new Vector3(-17, 17, 1);  //change localScale to have him face right

        }
        else if (aiPath.desiredVelocity.x <= 0.01f)  //if his speed is moving left
        {
            transform.localScale = new Vector3(17f, 17f, 1f);  //change localScale to have him face Lect
        }
    }
}
