using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    //List that hold all the shit.
    [SerializeField]
    private List<Weapon> database = new List<Weapon>();
    private JsonData data; //needed for Json. dont ask. duno.

    void Start()
    {
        /*
         * Converts text from *.json to objects in data.
         * Then populates the List. xd
         */
        data = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons.json")); 
        PopulateDatabase();
    }

    //Gives the weapon by id.
    public Weapon FetchWeaponByID(int id)
    {
        //Search through database.
        foreach (Weapon item in database)
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
        for (int i = 0; i < data.Count; i++)
        {
            database.Add(new Weapon((int)data[i]["id"], data[i]["title"].ToString(), (int)data[i]["damage"]));
        }
    }
}

