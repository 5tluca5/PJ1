using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalText
{
    public Vector3 targetPosition;
    public GameObject targetGO;
    public RectTransform rectTrans;

    public bool active;
    public GameObject gameObj;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gameObj.transform.localScale = Vector3.one;
        gameObj.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        gameObj.SetActive(active);
    }

    public void UpdateNormalText()
    {
        if (!active) return;

        if (Time.time - lastShown > duration)
            this.Hide();

        Vector3 targetPos = Camera.main.WorldToScreenPoint(targetGO.transform.position);
        Vector3 currentPos = rectTrans.anchoredPosition;

        Vector2 delta = targetPos - currentPos;

        if(delta != Vector2.zero)
        {
            rectTrans.anchoredPosition += new Vector2(delta.x * Time.deltaTime * 100, delta.y * Time.deltaTime * 100);
        }
        //rectTrans.position += motion * Time.deltaTime;
        


    }
}
