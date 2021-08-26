using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject menuObject;

    public void onButtonClicked()
    {
        if (menuObject.GetComponent<CanvasGroup>().alpha != 0)
        {
            // The menu is showing / visible
            return;
        }

        menuObject.GetComponent<Animator>().SetTrigger("Show");
    }
}
