using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Entry : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.name == "Boss")
        {
            animator.SetTrigger("Boss_Has_Fallen");
        }
    }
}
