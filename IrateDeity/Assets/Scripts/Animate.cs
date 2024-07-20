using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    Animator animator;

    public float horizontal;
    public float vertical;


    private void Awake()
    {
        if (animator == null || animator.isActiveAndEnabled == false)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        // if the animator component is not active, do not update
        if (animator == null || animator.isActiveAndEnabled == false)
        {
            // Debug.LogWarning("The GameObject has no Animator component.");
            return;
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

}
