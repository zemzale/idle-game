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

public class ArmorDatabase : MonoBehaviour {

    //List that hold all the shit.
    private List<Armor> armorDatabase = new List<Armor>();
    private JsonData armorData = new JsonData();

    void Awake()
    {
#if UNITY_EDITOR    //If compileing for editor.
        armorData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Armor.json"));
        PopulateArmorDatabase();
        DatabaseManager.singelton.OnDatabaseReady(DatabaseManager.singelton.armorDatabaseReady = true);
#elif UNITY_ANDROID //If compileing for Android.
        string armorPath = Application.streamingAssetsPath + "/Armor.json";
        StartCoroutine(GetJsonDataArmor(armorPath));
#endif
    }

    //Populateing data var for Android.
    private IEnumerator GetJsonDataArmor(string path)
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

            Debug.Log("Mapping JSON to armorData object!");
            armorData = JsonMapper.ToObject(jsonString);
            PopulateArmorDatabase();
        }
    }

    void PopulateArmorDatabase()
    {
        if (armorData != null)
        {
            for (int i = 0; i < armorData.Count; i++)
            {
                armorDatabase.Add(new Armor((int)armorData[i]["id"], armorData[i]["title"].ToString(), (int)armorData[i]["defense"], (int)armorData[i]["bonusHP"], (int)armorData[i]["dexterity"]));
            }
            DatabaseManager.singelton.OnDatabaseReady(DatabaseManager.singelton.armorDatabaseReady = true);
        }
        else
        {
            Debug.LogError("Trying to populate armor databases, but data file is empty!");
        }
    }

    //Gives the armor by id.
    public Armor FetchArmorByID(int id)
    {
        //Search through database.
        foreach (Armor item in armorDatabase)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        Debug.LogError("CANT FETCH ITEM BY ID!");
        return null;
    }
}
