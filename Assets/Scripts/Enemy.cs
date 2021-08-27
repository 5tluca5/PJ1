using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 0.16f;
    public float chaseLength = 0.48f;
    public float chaseSpeed = 0.5f;
    public float backSpeed = 0.4f;
    public float getBlockTime = 0.2f;
    private bool chasing;
    private bool collidingWithPlayer;
    private float getBlockTimer;
    private Vector2 getBlockDirection;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;

    protected override void Start()
    {
        base.Start();

        playerTransform = GameManager.instance.player.transform;
        startingPosition = this.transform.position;
        hitbox = GetComponentInChildren<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if(getBlockedHorizontally || getBlockedVertically)
        {
            if(getBlockTimer == 0.0f)
            {
                // Save the very first direction
                getBlockDirection = playerTransform.position - transform.position;

            }
            getBlockTimer += Time.deltaTime;
        }
        else
        {
            getBlockTimer = 0.0f;
            getBlockDirection = Vector2.zero;
        }

        // Check for overlaps
        collidingWithPlayer = false;

        hitbox.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null) continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }

        // Check if the player is in range
        if (Vector3.Distance(startingPosition, playerTransform.position) < triggerLength)
            chasing = true;

        if (Vector3.Distance(this.transform.position, playerTransform.position) < chaseLength)
        {
            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    // chasing
                    ChasePlayer();
                }
            }
            else
            {
                BackToStartingPosition();
            }
        }
        else
        {
            BackToStartingPosition();
            chasing = false;
        }

    }

    protected override void Death()
    {
        base.Death();

        Destroy(gameObject);
        GameManager.instance.AddExp(xpValue);
        GameManager.instance.ShowTextWithWorldSpace("+" + xpValue + " EXP", this.transform.position, Vector3.up * 40, 1.0f, 30, Color.magenta);
    }

    private void ChasePlayer()
    {
        Vector3 delta = playerTransform.position - this.transform.position;

        Vector3 chasingDirection = (delta.normalized);

        // Prevent infinite blocking
        if (getBlockTimer > getBlockTime && blockingObject != "Player")
        {
            if (getBlockedHorizontally && pushDirection == Vector3.zero) 
            {
                chasingDirection.y = (chaseSpeed) * Mathf.Sign(getBlockDirection.y);
            }
            else if (getBlockedVertically && pushDirection == Vector3.zero)
            {
                chasingDirection.x = (chaseSpeed) * Mathf.Sign(getBlockDirection.x);
            }

            if(getBlockedHorizontally && getBlockedVertically)
            {
                // Step back
                chasingDirection = (delta.normalized) * -2;
            }
        }

        this.UpdateMotor(chasingDirection * chaseSpeed);
    }

    private void BackToStartingPosition()
    {
        Vector3 delta = startingPosition - this.transform.position;

        // Prevent infinite movement
        if (Mathf.Abs(delta.x) < 0.01 && Mathf.Abs(delta.y) < 0.01) return;

        Vector3 backDirection = (delta.normalized);

        // Prevent infinite blocking
        if (getBlockTimer > getBlockTime)
        {
            if (getBlockedHorizontally && pushDirection == Vector3.zero)
            {
                backDirection.y = (chaseSpeed) * Mathf.Sign(getBlockDirection.y);
            }
            else if (getBlockedVertically && pushDirection == Vector3.zero)
            {
                backDirection.x = (chaseSpeed) * Mathf.Sign(getBlockDirection.x);
            }

            if (getBlockedHorizontally && getBlockedVertically)
            {
                // Step back
                backDirection = (delta.normalized) * -2;
            }
        }

        this.UpdateMotor(backDirection * backSpeed);
    }
        
}
