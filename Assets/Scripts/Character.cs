using UnityEngine;

[RequireComponent(typeof(CharacterUI))]
public class Character : MonoBehaviour {

    public string debugName;

    [SerializeField]
    private bool isPlayer;

    //ref to stat class
    public CharacterStats stats;
    //Geter so it returns only 0 or more.
    [SerializeField]
    private int currentHealth;

    //If u dead then tru else fals. U know how dis goes.
    private bool isDead = false;

    //ref to database
    private DatabaseManager database;

    //ref to armor and wep.
    [SerializeField]
    private Armor armor;
    public Weapon weapon;

    //GUI
    private CharacterUI ui;

    void Start()
    {
        
        ui = GetComponent<CharacterUI>();
        database = DatabaseManager.singelton;
        //u want wep. dis should changed to number that is linked to ur player acc or smth.

        EquipWeapon(1);
        EquipArmor(1);

        SetStats();
        SetDefaults();
    }

    #region Setup
    //Set player stats
    private void SetStats()
    {
        if (isPlayer)
        {
            stats = DatabaseManager.singelton.FetchPlayerStatsByName(debugName);
            ui.SetXpBar(stats.XP, stats.MaxXp);
            ui.SetLevelText(stats.LVL);
        }
        else
        {
            if (LevelManager.singelton != null)
            {
                stats = DatabaseManager.singelton.FetchPlayerStatsByName(LevelManager.singelton.NextEnemy());
            }
            else
            {
                stats = DatabaseManager.singelton.FetchPlayerStatsByName("Smarty");
            }
        }        
    }

    //equips weapon how u can see.
    //iff want some effects and gui updates when change wep. 
    //dat goes here.
    public void EquipWeapon(int _id)
    {
        weapon = database.FetchWeaponByID(_id);
        ui.SetWeaponImage(weapon.sprite);
    }

    //and same shit for armor
    public void EquipArmor(int _id)
    {
        armor = database.FetchArmorByID(_id);

    }
    #endregion


    #region Action
    //Called when u attack.
    public void Attack(Character obj)
    {
        //Dont attack if dead.lul
        if (!isDead)
        {
            float chance = 100 - ((0.1f * obj.stats.Dexterity) + obj.armor.dexterity) - Random.Range(0f, 10f) + ((0.1f * stats.Accuracy) + weapon.accuracy);
            Debug.Log("Chance to hit : " + chance);

            if (chance > 100)
            {
                obj.TakeDamage(DoDamage());
            }
            else
            {
                Debug.Log(transform.name + " missed!");
            }
        }
        
    }

    //Method called when its time to attack.
    //Callculates dmg and shit.
    public int DoDamage()
    {
        //TODO: Add calculation.
        float dmg = weapon.damage * stats.modDamage;

        Debug.Log(transform.name + " attacked for " + dmg);
        return (int)dmg;
    }

    //Method called when takeing damage.
    public void TakeDamage(int amount)
    {
        if (!isDead)
        {
            Debug.Log(transform.name + " took " + amount + " damage befor armor applied.");
            float armroValue  =  armor.defense * 0.75f * stats.modDefense;
            amount -= (int)armroValue;

            //Check so its not lower than 0
            if (currentHealth < amount)
                currentHealth = 0;
            else
                currentHealth -= amount;
            Debug.Log(transform.name + " took " + amount + " damage");
            ui.SetHealthBar(currentHealth, stats.Health + armor.bonusHP);

            //Check if health lower than 0 
            if (currentHealth <= 0)
            {
                //If yes then die.
                Die();
            }
        }
    }
    #endregion
   
    //Method for clean-up and hwatever.
    private void Die()
    {
        Debug.Log(transform.name + " died");
        isDead = true;
        //If is not player.
        if (!isPlayer)
        {
            //TODO : should make to a callback.
            Character obj = GameManager.singelton.GetPlayer();
            obj.stats.AddXp(stats.LVL * 60);
            obj.ui.SetXpBar(obj.stats.XP, obj.stats.MaxXp);
            Debug.Log("Added 60 xp to player! Now has " +  obj.stats.XP);
            obj.ui.SetLevelText(obj.stats.LVL);
            Respawn();
        }
        //If is player
        else
        {
            Character obj = GameManager.singelton.GetPlayer();
            obj.stats.LevelDown();
            ui.SetLevelText(stats.LVL);
        }
    }
    
    private void Respawn ()
    {
        Debug.Log("Respawning!");
        if (!isPlayer)
        {
            //TODO: Instead of debugName use some lvl manager.
            if (LevelManager.singelton == null)
            {
                Debug.LogError("No level manager!");
                return;
            }
            stats = DatabaseManager.singelton.FetchEnemyStatsByName(LevelManager.singelton.NextEnemy());
        }

        SetDefaults();
    }

    //later for cases if u suck and die.
    private void SetDefaults()
    {
        currentHealth = stats.Health + armor.bonusHP;
        isDead = false;
    }
}
