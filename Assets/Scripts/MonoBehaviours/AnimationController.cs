using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{

    private Animator animator;
   
    

    [SerializeField] private InputActionReference moveInput;
    private Rigidbody2D rb2d;

    

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        
    }

    private void Update()
    {
        Vector2 inputVector = moveInput.action.ReadValue<Vector2>();


        if(Mathf.Approximately(inputVector.x, 0) && Mathf.Approximately(inputVector.y, 0) )
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);


        }


        animator.SetFloat("xDir", inputVector.x);
        animator.SetFloat("yDir", inputVector.y);


    }

}
