using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefeb;

    private List<NormalText> normalTexts = new List<NormalText>();

    private void LateUpdate()
    {
        foreach (var txt in normalTexts)
        {
            txt.UpdateNormalText();
        }
    }

    //public void Show(string msg, Vector3 pos, Vector3 motion, float duration)
    //{
    //    this.Show(msg, pos, motion, duration, textPrefeb.GetComponent<Text>().fontSize, textPrefeb.GetComponent<Text>().color);
    //}

    public void Show(string msg, GameObject go, Vector3 motion, float duration, int fontSize, Color color)
    {
        NormalText normalText = this.GetNomralText();

        normalText.txt.text = msg;
        normalText.txt.fontSize = fontSize;
        normalText.txt.color = color;

        //normalText.targetPosition = Camera.main.WorldToScreenPoint(pos); // Transfer world space to screen space so we can ue it in the UI
        normalText.targetGO = go;
        normalText.rectTrans.position = normalText.targetPosition;
        normalText.motion = motion;
        normalText.duration = duration;

        normalText.Show();
    }

    private NormalText GetNomralText()
    {
        NormalText txt = normalTexts.Find(text => !text.active);

        if (txt == null)
        {
            txt = new NormalText();
            txt.gameObj = Instantiate(textPrefeb);
            txt.gameObj.transform.SetParent(textContainer.transform);
            txt.txt = txt.gameObj.GetComponent<Text>();
            txt.rectTrans = txt.gameObj.GetComponent<RectTransform>();

            normalTexts.Add(txt);
        }

        return txt;
    }
}
