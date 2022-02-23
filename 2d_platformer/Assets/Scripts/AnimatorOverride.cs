using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    
    private Animator animator;
    // Start is called before the first frame update

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown ("1")) 
        {
            Set(0);
        }

        if (Input.GetKeyDown ("2")) 
        {
            Set(1);
        }

        if (Input.GetKeyDown ("3")) 
        {
            Set(2);
        }

        if (Input.GetKeyDown ("4")) 
        {
            Set(3);
        }
    }

    public void Set(int value)
    {
        SetAnimations(overrideControllers[value]);
    }

    public void SetAnimations(AnimatorOverrideController overrideController)
    {
        animator.runtimeAnimatorController = overrideController;
    }
}
