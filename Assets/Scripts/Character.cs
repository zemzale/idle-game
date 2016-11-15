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
    private SaveManager saveManager;

    //ref to armor and wep.
    public Armor armor;
    public Weapon weapon;

    //GUI
    private CharacterUI ui;

    void Start()
    {
        //Seting up refrences.
        ui = GetComponent<CharacterUI>();
        database = DatabaseManager.singelton;
        saveManager = SaveManager.singelton;

        //Setting stats
        SetStats();
    }

    #region Setup
    
    //Set player stats
    private void SetStats()
    {
        //TODO: Move to check player/non-player and add predifined(???) values to enemies!
        EquipWeapon(saveManager.WeaponId);
        EquipArmor(saveManager.ArmorId);

        if (isPlayer)
        {
            stats = DatabaseManager.singelton.FetchPlayerStatsByName(debugName);
            SetDefaults();
            ui.SetXpBar(stats.XP, stats.MaxXp);
        }
        else
        {
            if (LevelManager.singelton != null)
                stats = DatabaseManager.singelton.FetchEnemyStatsByName(LevelManager.singelton.GetNextEnemy());
            else
                Debug.LogError("No level manager found! Cant set stats for Enemy.");           
        }

        if (stats != null)
        {
            SetGUI();
        }
        else
            Debug.LogError(this.transform.name + " has no stats!");
    }

    private void SetGUI ()
    {
        ui.SetCharacterImage(stats.Graphic);
        ui.SetLevelText(stats.LVL);
        ui.SetNameText(stats.Name);
    }

    //equips weapon how u can see.
    //iff want some effects and gui updates when change wep. 
    //dat goes here.
    private void EquipWeapon(int _id)
    {
        weapon = database.FetchWeaponByID(_id);
        ui.SetWeaponImage(weapon.sprite);
    }

    //and same shit for armor
    private void EquipArmor(int _id)
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
        //FIX: Add calculation.
        float dmg = weapon.damage * stats.modDamage;

        Debug.Log(transform.name + " attacked for " + dmg);
        return (int)dmg;
    }

    //Method called when takeing damage.
    public void TakeDamage(int amount)
    {
        //FIX: Make sure that there is no way of takeing negative damage!!!  

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
   
    //Method for clean-up and hwatever.
    private void Die()
    {
        Debug.Log(transform.name + " died");
        isDead = true;
        //If is not player.
        if (!isPlayer)
        {
            //TIDY: should make to a callback.

            /*
             * Gets Player and adds xp for the kill
             * if the we levelUp we reset HP and GUI update
             * then we respawn Enemy
             */
            Character player = GameManager.singelton.GetPlayer();
            int currentLvl = player.stats.LVL;
            player.stats.AddXp(Random.Range(60, 100));
            if (currentLvl < player.stats.LVL)
            {
                player.SetDefaults();
            }
               
            player.ui.SetXpBar(player.stats.XP, player.stats.MaxXp);
            player.ui.SetLevelText(player.stats.LVL);
            Respawn();
        }
        //If is player
        else
        {
            /*Level doww. Update UI.
             * Then reset stage and respawn.
             */
            stats.LevelDown();
            ui.SetLevelText(stats.LVL);
            ui.SetXpBar(stats.XP, stats.MaxXp);
            LevelManager.singelton.ResetStage();
            Respawn();
        }
    }
    
    private void Respawn ()
    {
        Debug.Log(this.name + " respawning!");
        if (LevelManager.singelton == null)
        {
            Debug.LogError("No level manager!");
            return;
        }

        if (!isPlayer)
        {
            /*
             * If not payer we get the next one and BOOOM
             * We get the name of Enemy from LevelManager and it does the shit.
            */
            string nextEnemy = LevelManager.singelton.GetNextEnemy();
            stats = DatabaseManager.singelton.FetchEnemyStatsByName(nextEnemy);
            //TODO: Add check if returns "DONE" by GetNextEnemy() cuz then everione ded. xd
            if (stats != null)
            {
                ui.SetNameText(stats.Name);
            }
            else
                Debug.LogError(this.transform.name + " has no stats!");
        }
        //Player is done already in Die()
        SetDefaults();
    }

    //later for cases if u suck and die.
    public void SetDefaults()
    {
        if (stats != null)
        {
            currentHealth = stats.Health + armor.bonusHP;
            isDead = false;
        }
        else
            Debug.LogError(this.transform.name + " has no stats!");
    }
    #endregion
}
