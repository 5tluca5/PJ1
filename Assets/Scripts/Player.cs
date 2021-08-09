using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 delta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        delta = new Vector3(x, y, 0);

        // Change moving direction
        if(delta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(delta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Check if there're any blocking object [left / right]
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(delta.x, 0), Mathf.Abs(delta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));

        if(hit.collider == null)
        {
            // Can move!
            transform.Translate(new Vector3(delta.x * Time.deltaTime, 0, 0));
        }

        // Check if there're any blocking object [top / bottom]
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, delta.y), Mathf.Abs(delta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));

        if (hit.collider == null)
        {
            // Can move!
            transform.Translate(new Vector3(0, delta.y * Time.deltaTime, 0));
        }
    }
}
