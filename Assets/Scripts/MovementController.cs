using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private float movementSpeed = 3.0f;

    private Vector2 movement = new Vector2();
    private bool isFacingRight = false;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Debug.Log(movement.x + " " + movement.y);
        Flip(movement);

    }

    private void FixedUpdate()
    {
        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;



    }



    private void Flip(Vector2 movement)
    {
        if (!isFacingRight && movement.x < 0f || isFacingRight && movement.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


}
