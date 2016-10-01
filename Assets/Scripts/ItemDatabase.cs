using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    //List that hold all the shit.
    [SerializeField]
    private List<Weapon> weaponDatabes = new List<Weapon>();
    private JsonData weaponData; //needed for Json. dont ask. duno.

    //Sameshit for armor.
    [SerializeField]
    private List<Armor> armorDatabase = new List<Armor>();
    private JsonData armorData;

    void Start()
    {
        /*
         * Converts text from *.json to objects in data.
         * Then populates the List. xd
         */
#if UNITY_EDITOR
        weaponData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons.json"));
        armorData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Armor.json"));
        PopulateDatabase();
#elif UNITY_ANDROID
        string path1 = Application.streamingDataPath + "/Weapons.json";
        string path2 = Application.streamingDataPath + "/Armor.json";
        StartCoroutine(GetJsonData(path1, weaponData));
        StartCoroutine(GetJsonData(path2, armorData));
#endif
    
    }

    private IEnumerator GetJsonData (string jsonUrl, JsonData data)
    {
        WWW www = new WWW(jsonUrl);

        yield return www;

        if(String.IsNullOrEmpty(www.err))
        {       
            Debug.Log("Writeing data : " + www.text + " to object!");   
            data = JsonMapper.ToObject(www.text);
            PopulateDatabase();
        }
        else
        {
            Debug.LogError("Can't open file at location : " + path);
        }
        
    }

    //Gives the weapon by id.
    public Weapon FetchWeaponByID(int id)
    {
        //Search through database.
        foreach (Weapon item in weaponDatabes)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        Debug.LogError("CANT FETCH ITEM BY ID!");
        return null;
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

    //U allready know.
    void PopulateDatabase()
    {
        if(weaponData != null)
        {
            for (int i = 0; i < weaponData.Count; i++)
                {
                weaponDatabes.Add(new Weapon((int)weaponData[i]["id"], weaponData[i]["title"].ToString(), (int)weaponData[i]["damage"], (int)weaponData[i]["speed"], (int)weaponData[i]["accuracy"]));
            }
        }
        if(armorData != null)
        {
            for (int i = 0; i < armorData.Count; i++)
            {
                armorDatabase.Add(new Armor((int)armorData[i]["id"], armorData[i]["title"].ToString(), (int)armorData[i]["defense"], (int)armorData[i]["bonusHP"], (int)armorData[i]["dexterity"]));
            }
        }
    }
}

