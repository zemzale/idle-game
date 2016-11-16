using UnityEngine;
using System.IO;
using System;

[RequireComponent(typeof(DatabaseManager))]
public class SaveManager : MonoBehaviour {

    public static SaveManager singelton;

    [SerializeField]
    private GameObject playerGO;
    private Character player;
    private CharacterStats stats;
    private int armorId;
    private int weaponId;
    private int stageId;
    private int playerLvl;
    private int playerXp;

    public int ArmorId
    {
        get
        {
            return armorId;
        }
    }
    public int WeaponId
    {
        get
        {
            return weaponId;
        }
    }
    public int PlayerLvl
    {
        get
        {
            return playerLvl;
        }
    }
    public int PlayerXp
    {
        get
        {
            return playerXp;
        }
    }

    private string filePath;

    void Start ()
    {
        if (singelton == null)
            singelton = this;
        else
            Debug.LogError("There is an SaveManager singelton already!");


        if (playerGO == null)
            Debug.LogWarning("No player found. Check inspector!");

        player = playerGO.GetComponent<Character>();
        filePath = Application.persistentDataPath + "/progress.kek";

        LoadGame();
    }

    public void SaveGame ()
    {
        armorId = player.armor.ID;
        weaponId = player.weapon.ID;
        stageId = GetComponent<LevelManager>().CurrentIndx;
        playerLvl = player.stats.LVL;
        playerXp = player.stats.XP;

        string[] toSave = { armorId.ToString(), weaponId.ToString(), stageId.ToString(),
                            playerLvl.ToString(), playerXp.ToString() };

        try
        {
            Debug.Log("Write contains : " + toSave[0] + " " + toSave[1] + " " + toSave[2] + " " + toSave[4] + " ");
            File.WriteAllLines(filePath, toSave);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void LoadGame ()
    {
        string[] statsText = new string[5];

        if (System.IO.File.Exists(filePath))
        {
            try
            {
                statsText = File.ReadAllLines(filePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }
        else
        {
            armorId = 1;
            weaponId = 1;
            GetComponent<LevelManager>().CurrentIndx = 1;
            playerLvl = 1;
            playerXp = 0;
            return;
        }

        armorId = Int32.Parse(statsText[0]);
        weaponId = Int32.Parse(statsText[1]);
        GetComponent<LevelManager>().CurrentIndx = Int32.Parse(statsText[2]);
        playerLvl = Int32.Parse(statsText[3]);
        playerXp = Int32.Parse(statsText[4]);


    }

}
