using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

/*
 * Armor database.
 * Here is setup of database and item fetching to manager.
 * Maybe(???) fetching could be moved only to DatabaseManager.
 */

public class EnemyDatabase : MonoBehaviour
{

    //List that hold all the shit.
    [SerializeField]
    private List<CharacterStats> enemyDatabase = new List<CharacterStats>();
    private JsonData enemyData = new JsonData();

    void Awake()
    {
#if UNITY_EDITOR    //If compileing for editor.
        enemyData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Enemies.json"));
        PopulateEnemyDataBase();
        DatabaseManager.singelton.OnDatabaseReady(DatabaseManager.singelton.enemyDatabaseReady = true);
#elif UNITY_ANDROID //If compileing for Android.
        string enemyPath = Application.streamingAssetsPath + "/Enemies.json";
        StartCoroutine(GetJsonDataEnemy(enemyPath));
#endif
    }

    //Populateing data var for Android.
    private IEnumerator GetJsonDataEnemy(string path)
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
            enemyData = JsonMapper.ToObject(jsonString);
            PopulateEnemyDataBase();
        }
    }

    void PopulateEnemyDataBase()
    {
        if (enemyData != null)
        {
            for (int i = 0; i < enemyData.Count; i++)
            { 
                enemyDatabase.Add(new CharacterStats(enemyData[i]["name"].ToString(), (int)enemyData[i]["health"], 
                                            (int)enemyData[i]["defense"],(int)enemyData[i]["damage"],
                                            (int)enemyData[i]["attackSpeed"], (int)enemyData[i]["accuracy"],
                                            (int)enemyData[i]["dexterity"], enemyData[i]["slug"].ToString()));
            }
            DatabaseManager.singelton.OnDatabaseReady(DatabaseManager.singelton.enemyDatabaseReady = true);
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
        foreach (CharacterStats item in enemyDatabase)
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