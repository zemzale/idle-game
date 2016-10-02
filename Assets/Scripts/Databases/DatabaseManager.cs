using UnityEngine;

/*
 * This script manages database set up
 * and makes sure the players get enabled only
 * after databases are setup
 * 
 * All database accsesing should be done trough here.
 */

 //Makes sure that object has these database scripts.
[RequireComponent(typeof(ArmorDatabase))]
[RequireComponent(typeof(WeaponDatabase))]
public class DatabaseManager : MonoBehaviour {

    //Singelton so can be accessed without refrence.
    public static DatabaseManager singelton;

    //Database refrences
    private ArmorDatabase armorDatabase;
    private WeaponDatabase weaponDatabase;

    private bool armorReady = false;
    private bool weaponReady = false;

    //Player array to enable after setup.
    [SerializeField]
    private Character[] characters;

    //Awake is called as the 1st method. Google Unity script execution order for more.
    void Awake ()
    {
        singelton = this;
        //Geting ze components
        armorDatabase = gameObject.GetComponent<ArmorDatabase>();
        weaponDatabase = gameObject.GetComponent<WeaponDatabase>();
    }

    public Armor FetchArmorByID(int id)
    {
        Armor item = armorDatabase.FetchArmorByID(id);
        if (item != null)
            return item;
        else
            return null;
    }

    public Weapon FetchWeaponByID(int id)
    {
        Weapon item = weaponDatabase.FetchWeaponByID(id);
        if (item != null)
            return item;
        else
            return null;
    }


    //Hook for when database is ready
    public void OnWeaponDatabaseReady ()
    {
        weaponReady = true;
        EnablePlayers();
    }
    public void OnArmorDatabaseReady()
    {
        armorReady = true;
        EnablePlayers();
    }

    //Makes sure that databases are ready and then enables players.
    private void EnablePlayers()
    {
        if (armorReady & weaponReady)
        {
            foreach (Character player in characters)
            {
                player.enabled = true;
            }
            Debug.Log("Players enabled.");
        }
    }
}
