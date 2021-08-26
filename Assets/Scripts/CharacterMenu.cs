using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text field
    public Text levelText, hpText, pesosText, upgradeCostText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            // If we went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;
        }
        else
        {
            currentCharacterSelection--;

            // If we went too far away
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }

        onSelectionChanged();
    }

    private void onSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        Debug.Log("Upgrade button clicked.");
    }


    // Update the character info
    public void updateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        upgradeCostText.text = "NOT IMPLEMENTED";

        // Mata
        //int playerLevel = GameManager.instance.player.
        int pesosAmount = GameManager.instance.pesos;
        int curHp = GameManager.instance.player.hitPoint;
        int maxHp = GameManager.instance.player.maxHitPoint;

        pesosText.text = pesosAmount.ToString();
        hpText.text = curHp + " / " + maxHp;
        levelText.text = "NOT IMPLEMENTED";

        // XP bar
        xpText.text = "NOT IMPLEMENTED";
        xpBar.localScale = new Vector3(0.5f, 1, 1);
    }

    private void Start()
    {
        
    }
}
