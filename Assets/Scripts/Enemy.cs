using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    public float chaseSpeed = 0.8f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();

        playerTransform = GameManager.instance.player.transform;
        startingPosition = this.transform.position;
        hitbox = GetComponentInChildren<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Check if the player is in range
        if (Vector3.Distance(startingPosition, playerTransform.position) < triggerLength)
            chasing = true;

        if (Vector3.Distance(startingPosition, playerTransform.position) < chaseLength)
        {
            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    // chasing
                    Vector3 chasingDirection = ((playerTransform.position - this.transform.position).normalized);

                    this.UpdateMotor(chasingDirection * chaseSpeed);
                }
            }
            else
            {
                this.UpdateMotor((startingPosition - transform.position) * chaseSpeed);
            }
        }
        else
        {
            //if (startingPosition != transform.position)
            //{
                this.UpdateMotor((startingPosition - transform.position) * chaseSpeed);
                chasing = false;
            //}
        }

        // Check for overlaps
        collidingWithPlayer = false;

        hitbox.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null) continue;

            if (hits[i].tag == "fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }

    protected override void Death()
    {
        base.Death();

        //Destroy(gameObject);
        //GameManager.instance.AddExp(xpValue);
        //GameManager.instance.ShowTextWithWorldSpace("+" + xpValue + " EXP", this.transform.position, Vector3.up * 40, 1.0f, 30, Color.magenta);
    }
}
