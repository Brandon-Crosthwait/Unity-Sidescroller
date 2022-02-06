using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private UnityEvent onContactTrigger;
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Timer.TimerOn = false;
            onContactTrigger.Invoke();
        }
    }
}