using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    
    public float maxHitPoints;
    public float startingHitPoints;

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();
    public abstract IEnumerator DamageCharacter(int damage, float interval);
    public virtual IEnumerator FlickerCharacter()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white; 

    }

}
