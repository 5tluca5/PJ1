using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover 
{
    private SpriteRenderer spriteRenderer;

    public int preferredSkin = 0;

    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        this.UpdateSkin(preferredSkin);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        this.UpdateMotor(new Vector3(x, y, 0));
    }

    public void UpdateSkin(int skinIndex)
    {
        preferredSkin = skinIndex;

        spriteRenderer.sprite = GameManager.instance.playerSprites[preferredSkin];
    }
}
