using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefeb;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (var txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }    
    }

    public void Show(string msg, Vector3 pos, Vector3 motion, float duration)
    {
        this.Show(msg, pos, motion, duration, textPrefeb.GetComponent<Text>().fontSize, textPrefeb.GetComponent<Text>().color);
    }

    public void Show(string msg, Vector3 pos, Vector3 motion, float duration, int fontSize, Color color)
    {
        FloatingText floatingText = this.GetFloatingText();

        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        //floatingText.gameObj.transform.position = Camera.main.WorldToScreenPoint(pos);  // Transfer world space to screen space so we can ue it in the UI
        floatingText.gameObj.transform.position = Camera.main.WorldToScreenPoint(pos);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(text => !text.active);

        if(txt == null)
        {
            txt = new FloatingText();
            txt.gameObj = Instantiate(textPrefeb);
            txt.gameObj.transform.SetParent(textContainer.transform);
            txt.txt = txt.gameObj.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
