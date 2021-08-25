using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover 
{
    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();    
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        this.UpdateMotor(new Vector3(x, y, 0));
    }
}
