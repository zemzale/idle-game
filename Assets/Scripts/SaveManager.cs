using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    private CharacterStats stats;
    private int armorId;
    private int weaponId;
    private int stageId;

    private string filePath;



    void Start ()
    {
        if (player == null)
            Debug.LogWarning("No player found. Check inspector!");

        filePath = Application.persistentDataPath + "/progress.kek";

        LoadGame();
    }

    public void SaveGame ()
    {
        stats = player.GetComponent<Character>().stats;

        try
        {
            File.WriteAllLines(filePath, stats.GetStatString());
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void LoadGame ()
    {
        string[] statsText = new string[99];

        try
        {
            statsText = File.ReadAllLines(filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

        foreach (string s in statsText)
        {
            Debug.Log(s);
        }

    }


}
