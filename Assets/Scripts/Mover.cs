using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 moveDelta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    private Collider2D[] hits = new Collider2D[10];
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1f;
    protected bool getBlockedHorizontally;
    protected bool getBlockedVertically;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        bool canMoveHorizontally = true;
        bool canMoveVertically = true;

        // Change moving direction
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Check what player are colliding
        boxCollider.OverlapCollider(new ContactFilter2D(), hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null) continue;

            // do sth
            // ...

            hits[i] = null;
        }

        // Check if there're any blocking object [left / right]
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor", "Stairs"));

        if (hit.collider == null && canMoveHorizontally)
        {
            // Can move!
            transform.Translate(new Vector3(moveDelta.x * Time.deltaTime, 0, 0));
            getBlockedHorizontally = false;
        }
        else
        {
            getBlockedHorizontally = true;
        }

        // Check if there're any blocking object [top / bottom]
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));

        if (hit.collider == null && canMoveVertically)
        {
            // Can move!
            transform.Translate(new Vector3(0, moveDelta.y * Time.deltaTime, 0));
            getBlockedVertically = false;
        }
        else
        {
            getBlockedVertically = true;
        }
    }

}
