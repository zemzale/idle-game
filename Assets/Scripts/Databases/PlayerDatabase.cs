using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

/*
 * Player stat database.
 * Here is setup of database and item fetching to manager.
 * Maybe(???) fetching could be moved only to DatabaseManager.
 */

public class PlayerDatabase : MonoBehaviour
{

    //List that hold all the shit.
    [SerializeField]
    private List<CharacterStats> playerDatabase = new List<CharacterStats>();
    private JsonData playerData = new JsonData();

    void Awake()
    {
#if UNITY_EDITOR    //If compileing for editor.
        playerData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Players.json"));
        PopulatePlayerDatabase();
        DatabaseManager.singelton.OnDatabaseReady(DatabaseManager.singelton.playerDatabaseReady = true);
#elif UNITY_ANDROID //If compileing for Android.
        string enemyPath = Application.streamingAssetsPath + "/Players.json";
        StartCoroutine(GetJsonDataPlayer(enemyPath));
#endif
    }

    //Populateing data var for Android.
    private IEnumerator GetJsonDataPlayer(string path)
    {
        Debug.Log("Trying to populate dataWeapon from path : " + path);
        WWW www = new WWW(path);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("Cant read!");
        }
        else
        {
            string jsonString = www.text;

            Debug.Log("Mapping JSON to enemyData object!");
            playerData = JsonMapper.ToObject(jsonString);
            PopulatePlayerDatabase();
        }
    }

    void PopulatePlayerDatabase()
    {
        if (playerData != null)
        {
            for (int i = 0; i < playerData.Count; i++)
            {
                playerDatabase.Add(new CharacterStats(playerData[i]["name"].ToString(), (int)playerData[i]["health"],
                                            (int)playerData[i]["defense"], (int)playerData[i]["damage"],
                                            (int)playerData[i]["attackSpeed"], (int)playerData[i]["accuracy"],
                                            (int)playerData[i]["dexterity"]));
            }
            DatabaseManager.singelton.OnDatabaseReady(DatabaseManager.singelton.playerDatabaseReady = true);
        }
        else
        {
            Debug.LogError("Trying to populate enemy databases, but data file is empty!");
        }
    }

    //Gives the armor by id.
    public CharacterStats FetchStatsByName(string name)
    {
        //Search through database.
        foreach (CharacterStats item in playerDatabase)
        {
            if (item.Name == name)
            {
                return item;
            }
        }
        Debug.LogError("CANT FETCH STATS BY NAME!");
        return null;
    }
}