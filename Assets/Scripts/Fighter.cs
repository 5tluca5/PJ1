using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public field
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    protected float immuneTime = 0.5f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    // All fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (this.transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowTextWithWorldSpace(("-" + dmg.damageAmount.ToString()).Bold(), transform.position, Vector3.up * 10, 0.5f, 25, Color.red);

            if(hitPoint <= 0)
            {
                hitPoint = 0;

                this.Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
