using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Flighter" && coll.name == "Player")
        {
            // Create a damage object, then send it to the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                pushForce = this.pushForce,
                origin = this.transform.position
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
