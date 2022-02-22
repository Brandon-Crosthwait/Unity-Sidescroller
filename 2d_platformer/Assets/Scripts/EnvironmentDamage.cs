using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnvironmentDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Health playerHealth;

    [SerializeField] private EnvironmentDamageHelper edHelper ;

    private void Awake() 
    {
        //edHelper = new EnvironmentDamageHelper();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        edHelper.playerTakesDamage(damage, collision);
    }
}

[System.Serializable]
public class EnvironmentDamageHelper
{
    [SerializeField] FloatEvent playerTakeDamage;
    [SerializeField] UnityEvent playerGetsHit;

    public bool wereEventsTriggered {get; set;}
    public void playerTakesDamage(float damage, Collider2D player)
    {
        
        wereEventsTriggered = false;
        if(player.tag == "Player" && player.GetComponent<Health>().currentHealth != 0)
        {
            try
            {
                playerTakeDamage.Invoke(damage);
                playerGetsHit.Invoke();
                
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e);
            }
            // player.GetComponent<Health>().TakeDamage(damage);
            // player.GetComponent<PlayerMovement>().GetHit();
            // ScoreScript.scoreValue -= 5;
            wereEventsTriggered = true;
        }
    }
}
