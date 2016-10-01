using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using LitJson;
using System.IO;

/*
 * Weapon database.
 * Here is setup of database and item fetching to manager.
 * Maybe(???) fetching could be moved only to DatabaseManager.
 */

public class WeaponDatabase : MonoBehaviour {
    
    //List that hold all the shit.
    private List<Weapon> weaponDatabes = new List<Weapon>();
    private JsonData weaponData; //needed for Json. dont ask. duno.

    
    void Awake()
    {
#if UNITY_EDITOR      //If compileing for editor.
        weaponData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons.json"));
        PopulateWeaponDatabase();
        DatabaseManager.singelton.OnWeaponDatabaseReady();
#elif UNITY_ANDROID   //If compileing for Android.
        string weaponPath = Application.streamingAssetsPath + "/Weapons.json";
        StartCoroutine(GetJsonDataWeapon(weaponPath));
#endif
    }

    //Populateing data var for Android.
    private IEnumerator GetJsonDataWeapon(string path)
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
            string jsonString = System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);

            Debug.Log("Mapping JSON to weaponData object!");
            weaponData = JsonMapper.ToObject(jsonString);
            PopulateWeaponDatabase();

        }
    }

    //U allready know.
    void PopulateWeaponDatabase()
    {
        if (weaponData != null)
        {
            for (int i = 0; i < weaponData.Count; i++)
            {
                weaponDatabes.Add(new Weapon((int)weaponData[i]["id"], weaponData[i]["title"].ToString(), (int)weaponData[i]["damage"], (int)weaponData[i]["speed"], (int)weaponData[i]["accuracy"]));
            }
            DatabaseManager.singelton.OnWeaponDatabaseReady();
        }
        else
        {
            Debug.LogError("Trying to populate weapon databases, but data file is empty!");
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

}
