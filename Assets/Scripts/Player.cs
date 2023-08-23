using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : Character
{

    [SerializeField] private GameInput gameInput;

    [SerializeField] private float movementSpeed = 3.0f;
    
    private Rigidbody2D rb2D;
    private Animator animator;




    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {


        HandleMovement();
        
    }

    //This function is used to flip sprite of character but not use for this anymore
    //private void Flip(Vector2 movement)
    //{
    //    if (!isFacingRight && movement.x < 0f || isFacingRight && movement.x > 0f)
    //    {
    //        isFacingRight = !isFacingRight;
    //        Vector2 localScale = transform.localScale;
    //        localScale.x *= -1f;
    //        transform.localScale = localScale;
    //    }
    //}
        

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();  

        Vector3 moveDir = new Vector2(inputVector.x, inputVector.y);

        //Debug.Log(moveDir.x + " " + moveDir.y);

        float moveDistance = movementSpeed * Time.deltaTime;


        transform.position += moveDir * moveDistance;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CanBePickUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if(hitObject != null)
            {
                switch(hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        //do something when hit coin
                        break;
                    case Item.ItemType.HEALTH:
                        //adjust hitpoint when hit health
                        AdjustHitPoints(hitObject.quantity);
                        collision.gameObject.SetActive(false);
                        break;
                }

            }
            else
            {
                Debug.Log("Error when hit object");
            }

        }
    }

    private void AdjustHitPoints(int quantity)
    {
        if(hitPoints < maxHitPoints)
        {
            Debug.Log("Current hitpoint: " + hitPoints);
            hitPoints += quantity;
            Debug.Log("Adjust hitpoint by " + quantity + " new value: " + hitPoints);
        }
       
    }

        

}
