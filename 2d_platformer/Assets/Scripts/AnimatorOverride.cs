using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    private void Start() 
    {
        SetAnimations(overrideControllers[PlayerPrefs.GetInt("CharacterAnimatorOverriderID")]);    
    }

    public void Set(int value)
    {
        PlayerPrefs.SetInt("CharacterAnimatorOverriderID", value);
    }

    public void SetAnimations(AnimatorOverrideController overrideController)
    {
        animator.runtimeAnimatorController = overrideController;
    }
}
