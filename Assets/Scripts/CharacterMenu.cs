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

    private void Start()
    {
        this.updateMenu();
    }

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

        GameManager.instance.player.UpdateSkin(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        Debug.Log("Upgrade button clicked.");

        if(GameManager.instance.TryUpgradeWeapon())
        {
            updateMenu();
        }
    }


    // Update the character info
    public void updateMenu()
    {
        Debug.Log("Update menu.");

        // Weapon
        int weaponLevel = GameManager.instance.weapon.weaponLevel;

        weaponSprite.sprite = GameManager.instance.weaponSprites[weaponLevel];

        if (weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[weaponLevel].ToString();

        // Skin
        int skinIndex = GameManager.instance.player.preferredSkin;

        characterSelectionSprite.sprite = GameManager.instance.playerSprites[skinIndex];

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
}
