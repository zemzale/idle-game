using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterUI))]
public class Character : MonoBehaviour {

    //ref to stat class
    public CharacterStats stats;
    //Geter so it returns only 0 or more.
    [SerializeField]
    private int currentHealth;

    //If u dead then tru else fals. U know how dis goes.
    protected bool isDead = false;

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


        //Dis too should change later.
        currentHealth = stats.Health + armor.bonusHP;
        
    }

    //later for cases if u suck and die.
    protected void SetDefaults()
    {
        currentHealth = stats.Health;
        isDead = true;
    }

    //Called when u attack.
    public void Attack(Character obj)
    {
        //Dont attack if dead.lul
        if (!isDead)
        {
            float chance = 100 - ((0.1f * stats.Dexterity) + armor.dexterity) - Random.Range(0f, 10f) + ((0.1f * stats.Accuracy) + weapon.accuracy);
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
            ui.SetHealthBar(currentHealth, stats.Health);

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

}
