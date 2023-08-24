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

    public HealthBar healthBarPrefab;

    HealthBar healthBar;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);

        healthBar.character = this;
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
                bool shouldDisappear = false;   
                switch(hitObject.itemType)
                {

                    case Item.ItemType.COIN:
                        //do something when hit coin
                        shouldDisappear = true;
                        break;
                    case Item.ItemType.HEALTH:
                        //adjust hitpoint when hit health
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }

                if(shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }

            }
            else
            {
                Debug.Log("Error when hit object");
            }

        }
    }

    private bool AdjustHitPoints(int quantity)
    {  

        if (hitPoints.value < maxHitPoints)
        {
            Debug.Log("Current hitpoint: " + hitPoints);
            hitPoints.value += quantity;
            Debug.Log("Adjust hitpoint by " + quantity + " new value: " + hitPoints);
            return true;
        }

        return false;
    }

        

}
