using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftOffAnimator : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetInteger("AnimationState", 0); // idle test
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetInteger("AnimationState", 1); // walking test
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetInteger("AnimationState", 2); // hook punch test
        }
    }
}
