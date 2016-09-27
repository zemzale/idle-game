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
#elif UNITY_ANDROID
        string path1 = "jar::file://" + Application.dataPath + "!/assets/Weapons.json";
        string path2 = "jar::file://" + Application.dataPath + "!/assets/Armor.json";
        StartCoroutine(GetJsonData(path1, weaponData));
        StartCoroutine(GetJsonData(path2, armorData));
#endif
        PopulateDatabase();
    }

    private IEnumerator GetJsonData (string jsonUrl, JsonData data)
    {
        WWW www = new WWW(jsonUrl);

        yield return www;

        data = JsonMapper.ToObject(www.text);
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
        for (int i = 0; i < weaponData.Count; i++)
        {
            weaponDatabes.Add(new Weapon((int)weaponData[i]["id"], weaponData[i]["title"].ToString(), (int)weaponData[i]["damage"], (int)weaponData[i]["speed"], (int)weaponData[i]["accuracy"]));
        }
        for (int i = 0; i < armorData.Count; i++)
        {
            armorDatabase.Add(new Armor((int)armorData[i]["id"], armorData[i]["title"].ToString(), (int)armorData[i]["defense"], (int)armorData[i]["bonusHP"], (int)armorData[i]["dexterity"]));
        }
    }
}

