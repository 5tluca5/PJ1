using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (collected) return;

        base.OnCollect();

        GetComponent<SpriteRenderer>().sprite = emptyChest;

        Debug.Log("Chest collected.");
        Debug.Log("Granted " + pesosAmount + " pesos.");
    }
}
