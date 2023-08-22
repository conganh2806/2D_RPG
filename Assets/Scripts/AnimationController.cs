using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Animator animator;
   
    private string animationState = "isRunning";

    [SerializeField] private Transform playerParentTransform;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        rb2d = playerParentTransform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb2d.velocity != Vector2.zero)
        {
            animator.SetBool(animationState, true);
            //Debug.Log("Character is running!");
        } 
        else
        {
            animator.SetBool(animationState, false);
            //Debug.Log("Character is idling!");
        }
            

    }

}
