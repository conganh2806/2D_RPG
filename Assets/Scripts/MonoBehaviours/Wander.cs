using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    [SerializeField] private float pursuitSpeed;

    [SerializeField] private float wanderSpeed;
    [SerializeField] private float currentSpeed;

    [SerializeField] private float directionChangeInterval;

    public bool followPlayer;

    Coroutine moveCoroutine;

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    private Transform targetTransform = null;

    private Vector3 endPosition;

    private float currentAngle = 0;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentSpeed = wanderSpeed;
        StartCoroutine(WanderRoutine());

    }

    private IEnumerator WanderRoutine()
    {
        while(true)
        {
            ChooseNewEndPoint();

            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            
            //moveCoroutine = StartCoroutine(Move(rb2d, wanderSpeed));
            


            yield return new WaitForSeconds(directionChangeInterval);

        }
    }

    private void ChooseNewEndPoint()
    {
        currentAngle += UnityEngine.Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);

        endPosition += Vector3FromAngle(currentAngle);

    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        // 1
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        // 2
        return new Vector3(Mathf.Cos(inputAngleRadians),
       Mathf.Sin(inputAngleRadians), 0);
    }


    //private IEnumerator Move(Rigidbody2D rb2d, float wanderSpeed)
    //{

    //}
}
