using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
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
        gameObj.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        gameObj.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active) return;

        if (Time.time - lastShown > duration)
            this.Hide();

        gameObj.transform.position += motion * Time.deltaTime;

        if(Time.time - lastShown > duration * 0.5)
        {
            // Fade out
            Color txtColor = gameObj.GetComponent<Text>().color;

            float fadeValue =  1 / (duration * 0.5f) * Time.deltaTime;

            gameObj.GetComponent<Text>().color = new Color(txtColor.r, txtColor.g, txtColor.b, txtColor.a - fadeValue);
        }
        
    }
}
