using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Status : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 10;

    

    /// <summary>
    /// deplete the boss health if we hit him with the projectile
    /// </summary>
    public void DepleteHealth()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
