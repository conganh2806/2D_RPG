using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Animator animator;
   
    private string animationState = "AnimationState";

    [SerializeField] private GameInput gameInput;
    private Rigidbody2D rb2d;

    private enum CharStates
    {
        walkEast = 1,
        walkWest = 2,
        walkNorth = 3,
        walkSouth = 4,  
        idle = 5,
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        if (inputVector.x > 0)
        {
            animator.SetInteger(animationState, 1);
            //Debug.Log("Character is running!");
        }
        else if (inputVector.x < 0)
        {
            animator.SetInteger(animationState, 2);
            //Debug.Log("Character is idling!");
        }
        else if (inputVector.y > 0)
        {
            animator.SetInteger(animationState, 3);

        }
        else if (inputVector.y < 0)
        {
            animator.SetInteger(animationState, 4);
        }
        else
        {
            animator.SetInteger(animationState, 5);
        }


    }

}
