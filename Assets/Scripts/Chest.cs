using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (collected)
        {
            GameManager.instance.ShowText("It's empty.",Camera.main.WorldToScreenPoint(this.transform.position) + new Vector3(0, 40, 0), Vector3.zero, 1f);
            return;
        }

        base.OnCollect();

        GetComponent<SpriteRenderer>().sprite = emptyChest;

        GameManager.instance.AddPesos(pesosAmount);

        Debug.Log("Chest collected.");
        //Debug.Log("Granted " + pesosAmount + " pesos.");
        GameManager.instance.ShowTextWithWorldSpace("+" + pesosAmount + " pesos!", this.transform.position, Vector3.up * 10, 1.5f, 25, Color.yellow);

    }
}
