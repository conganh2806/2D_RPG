using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float hitPoints;
    [SerializeField] private int damageStrength;
    [SerializeField] private float damageInterval = 1.0f;
    Coroutine damageCoroutine;

    private Vector2 lastPosition;
    private bool isFacingRight;

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while(true)
        {
            StartCoroutine(FlickerCharacter());

            hitPoints = hitPoints - damage;

            if(hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if(interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();   
    }

    private void Update()
    {
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(damageCoroutine == null)
            {
                
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, damageInterval));
         
            
            }
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    


    private void Flip()
    {
        Vector2 currentPosition = transform.position;

        if(currentPosition != lastPosition)
        {
            if(!isFacingRight && currentPosition.x - lastPosition.x < 0 || isFacingRight && currentPosition.x - lastPosition.x > 0)
            {
                isFacingRight = !isFacingRight;
                Vector2 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
          
        }


        lastPosition = currentPosition;

    }


    


}
