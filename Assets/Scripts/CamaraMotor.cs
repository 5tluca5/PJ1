using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;    // 15 pixel boundary
    public float boundY = 0.05f;    // 5 pixel boundary


    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        // Tracing target directly
        //transform.position = new Vector3(lookAt.position.x, lookAt.position.y, transform.position.z);

        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;

        if(Mathf.Abs(deltaX) > boundX)
        {
            // Target went out the boundary
            if (lookAt.position.x < transform.position.x)
            {
                delta.x = deltaX + boundX;
          }
            else
            {
                delta.x = deltaX - boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;

        if (Mathf.Abs(deltaY) > boundY)
        {
            // Target went out the boundary
            if (lookAt.position.y < transform.position.y)
            {
                delta.y = deltaY + boundY;
            }
            else
            {
                delta.y = deltaY - boundY;
            }
        }

        // Move the camara
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
