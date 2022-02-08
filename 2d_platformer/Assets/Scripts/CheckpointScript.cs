using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private UnityEvent onContactTrigger;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            onContactTrigger.Invoke();
        }
    }

    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
