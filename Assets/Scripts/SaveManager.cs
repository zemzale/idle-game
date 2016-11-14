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

        string[] toSave = { armorId.ToString(), weaponId.ToString(), stageId.ToString() };

        try
        {
            File.WriteAllLines(filePath, toSave);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void LoadGame ()
    {
        string[] statsText = new string[3];

        try
        {
            statsText = File.ReadAllLines(filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

        armorId = Int32.Parse(statsText[0]);
        weaponId = Int32.Parse(statsText[1]);
        GetComponent<LevelManager>().CurrentIndx = Int32.Parse(statsText[2]);

    }


}
