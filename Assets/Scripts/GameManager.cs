using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices = new List<int>{ 100, 200, 500, 1000, 2000, 4000, 8000 };
    public List<int> xpTable;

    // Reference
    public Player player;
    public Weapon weapon;
    public FloatingTextManager FTM;
    public NormalTextManager NTM;

    // Logic
    public int pesos = 0;
    public int experience = 0;

    private void Start()
    {
        // Auto saveState every 5 sec
        InvokeRepeating("SaveState", 5f, 5f);
    }

    public void AddPesos(int amount)
    {
        pesos += amount;

        this.SaveState();
    }

    public void AddExp(int amount)
    {
        experience += amount;

        this.SaveState();
    }

    public void ShowText(string msg, Vector3 pos, Vector3 motion, float duration)
    {
        FTM.Show(msg, pos, motion, duration);
    }

    public void ShowTextWithWorldSpace(string msg, Vector3 pos, Vector3 motion, float duration, int fontSize, Color color)
    {
        FTM.ShowWithWorldSpace(msg, pos, motion, duration, fontSize, color);
    }

    public void ShowText(string msg, Vector3 pos, Vector3 motion, float duration, int fontSize, Color color)
    {
        FTM.Show(msg, pos, motion, duration, fontSize, color);
    }

    public void ShowText(string msg, GameObject go, Vector3 motion, float duration, int fontSize, Color color)
    {
        NTM.Show(msg, go, motion, duration, fontSize, color);
    }

    // Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        // Check if wepaon was max level
        if (weapon.weaponLevel >= weaponPrices.Count) return false;

        // Check if have enough pesos
        if (weaponPrices[weapon.weaponLevel] > pesos) return false;

        this.AddPesos(-weaponPrices[weapon.weaponLevel]);
        weapon.UpgradeWeapon();

        return true;
    }

    /// <SaveState>
    /// INT preferedSkin
    /// INT pesos   
    /// INT experience
    /// INT weaponLevel
    /// </SaveState>

    public void SaveState()
    {
        //Debug.Log("SaveState");

        string s = "";

        s += player.preferredSkin.ToString().AddSplit();
        s += pesos.ToString().AddSplit();
        s += experience.ToString().AddSplit();
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);


        //Debug.Log("Data stored: " + s);
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("LoadState");

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            // No saved data yet
            Debug.Log("No Saved data found.");
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        player.preferredSkin = int.Parse(data[0]);
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.weaponLevel = int.Parse(data[3]);

        //Debug.Log("Data load succeed: " + PlayerPrefs.GetString("SaveState"));
    }
}
