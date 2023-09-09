using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    bool isFiring;
    [HideInInspector] public Animator animator;

    private Camera localCamera;

    private float positiveSlope;
    private float negativeSlope;

    private enum Quadrant
    {
        EAST,
        WEST,
        SOUTH,
        NORTH
    }


    [SerializeField] private GameObject ammoPrefab;

    private List<GameObject> ammoPool;

    [SerializeField] private int poolSize;
    [SerializeField] private float weaponVelocity;

    private void Start()
    {
        animator = GetComponent<Animator>();

        isFiring = false;

        localCamera = Camera.main;

        Vector2 lowerLeft = localCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        positiveSlope = GetSlope(lowerLeft, upperRight);
        negativeSlope = GetSlope(upperLeft, lowerRight);
    }

    private void Awake()
    {
        if(ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }
        for(int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }

    }

    private void Update()
    {
        if(UnityEngine.Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            FireAmmo();
        }

        UpdateState();
    }

    private GameObject SpawnAmmo(Vector3 location)
    {
        foreach(GameObject ammo in ammoPool)
        {
            if(ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }

        }
        return null;
    }

    private void FireAmmo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

        GameObject ammo = SpawnAmmo(transform.position);
        if(ammo != null)
        {
            Arc arcScript = ammo.GetComponent<Arc>();

            float travelDuration = 1.0f / weaponVelocity;

            StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
        }

    }

    private float GetSlope(Vector2 pointOne, Vector2 pointTwo)
    {
        return (pointTwo.y - pointOne.y) / (pointTwo.x - pointOne.x);
    }

    private bool HigherThanPositiveSlopeLine(Vector2 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;

        Vector2 mousePosition = localCamera.ScreenToWorldPoint(inputPosition);

        float yIntercept = playerPosition.y - (positiveSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (positiveSlope * mousePosition.x);

        return inputIntercept > yIntercept; 



    }

    bool HigherThanNegativeSlopeLine(Vector2 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(inputPosition);
        
        float yIntercept = playerPosition.y - (negativeSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (negativeSlope * mousePosition.x);

        return inputIntercept > yIntercept;
    }


    //The GetQuadrant() method is responsible for determining which 
    //of the four quadrants the user has tapped in and returning a
    //Quadrant.
    Quadrant GetQuadrant()
    {
        Vector2 mousePosition = UnityEngine.Input.mousePosition;
        Vector2 playerPosition = transform.position;

        bool higherThanPositiveSlopeLine = HigherThanPositiveSlopeLine(mousePosition);
        bool higherThanNegativeSlopeLine = HigherThanNegativeSlopeLine(mousePosition);

        Debug.Log(higherThanPositiveSlopeLine + "  " + higherThanNegativeSlopeLine);
        if (!higherThanPositiveSlopeLine && higherThanNegativeSlopeLine)
        {
            return Quadrant.EAST;
        }
        else if (higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine)
        {
            return Quadrant.WEST;
        }
        else if (higherThanPositiveSlopeLine && higherThanNegativeSlopeLine)
        {
            return Quadrant.NORTH;
        }
        else
        {
            return Quadrant.SOUTH;
        }

    }


    private void UpdateState()
    {
        if(isFiring)
        {
            Vector2 quadrantVector = new Vector2(0.0f, 0.0f);

            Quadrant quadEnum = GetQuadrant();
            Debug.Log(quadEnum.ToString());

            switch(quadEnum)
            {
                case Quadrant.EAST:
                    quadrantVector = new Vector2(1.0f, 0.0f);
                    break;
                case Quadrant.WEST:
                    quadrantVector = new Vector2(-1.0f, 0.0f);
                    break;
                case Quadrant.NORTH:
                    quadrantVector = new Vector2(0.0f, 1.0f);
                    break;
                case Quadrant.SOUTH:
                    quadrantVector = new Vector2(0.0f, -1.0f);
                    break;

            }

            Debug.Log(quadrantVector.x + " " + quadrantVector.y);

            animator.SetBool("isFiring", true);

            animator.SetFloat("fireXDir", quadrantVector.x);
            animator.SetFloat("fireYDir", quadrantVector.y);

            isFiring = false;

        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }



}
