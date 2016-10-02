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
[RequireComponent(typeof(EnemyDatabase))]
[RequireComponent(typeof(PlayerDatabase))]
public class DatabaseManager : MonoBehaviour {

    //Singelton so can be accessed without refrence.
    public static DatabaseManager singelton;

    //Database refrences
    private ArmorDatabase armorDatabase;
    private WeaponDatabase weaponDatabase;
    private EnemyDatabase enemyDatabase;
    private PlayerDatabase playerDatabase;

    public bool armorDatabaseReady = false;
    public bool weaponDatabaseReady = false;
    public bool enemyDatabaseReady = false;
    public bool playerDatabaseReady = false;


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
        enemyDatabase = gameObject.GetComponent<EnemyDatabase>();
        playerDatabase = gameObject.GetComponent<PlayerDatabase>();
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
    
    public CharacterStats FetchEnemyStatsByName(string name)
    {
        CharacterStats item = enemyDatabase.FetchStatsByName(name);
        if (item != null)
            return item;
        else
            return null;
    }

    public CharacterStats FetchPlayerStatsByName(string name)
    {
        CharacterStats item = playerDatabase.FetchStatsByName(name);
        if (item != null)
            return item;
        else
            return null;
    }


    //Hook for when database is ready
    public void OnDatabaseReady(bool database)
    {
        database = true;
        EnablePlayers();
    }
    

    //Makes sure that databases are ready and then enables players.
    private void EnablePlayers()
    {
        if (armorDatabaseReady & weaponDatabaseReady & enemyDatabaseReady)
        {
            foreach (Character player in characters)
            {
                player.enabled = true;
            }
            Debug.Log("Players enabled.");
        }
    }
}
