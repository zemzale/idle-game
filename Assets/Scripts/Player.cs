using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //ref to stat class
    public PlayerStats stats;
    [SerializeField]
    protected int currentHealth;

    //If u dead then tru else fals. U know how dis goes.
    protected bool isDead = false;

    //ref to database
    private DatabaseManager database;

    //ref to armor and wep.
    [SerializeField]
    private Armor armor;
    public Weapon weapon;
   
    //and healthbar ref.
    [SerializeField]
    private RectTransform healthBar;

    //Weapone image ref.
    [SerializeField]
    private Image weaponeImage;




    void Start()
    {
        database = DatabaseManager.singelton;
        //u want wep. dis should changed to number that is linked to ur player acc or smth.

        EquipWeapon(1);
        EquipArmor(1);


        //Dis too should change later.
        currentHealth = stats.Health + armor.bonusHP;
        
        //if no healthbar u done goof. addd in inspector knub.
        if (healthBar == null)
        {
            Debug.LogError("No health bar!");
        }
    }

    //later for cases if u suck and die.
    protected void SetDefaults()
    {
        currentHealth = stats.Health;
        isDead = true;
    }

    //Called when u attack.
    public void Attack(Player obj)
    {
        float chance = 100 - ((0.1f * stats.Dexterity) + armor.dexterity) - Random.Range(0f, 10f) + ((0.1f * stats.Accuracy) + weapon.accuracy);
        Debug.Log("Chance to hit : "  +  chance);

        if (chance >  100)
        {
            obj.TakeDamage(DoDamage());
        }
        else
        {
            Debug.Log(transform.name + " missed!");
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
            float armorNum  =  armor.defense * 0.75f * stats.modDefense;
            amount -= (int)armorNum;
            currentHealth -= amount;
            Debug.Log(transform.name + " took " + amount + " damage");
            SetHealthBar();

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


    //GUI

    //Sets healthbar size.
    private void SetHealthBar()
    {
        //Returns health in %%%%%%%%%%%%%% 
        float barScale = ((currentHealth * 100) / stats.Health) * 0.01f;
        if (barScale < 0)
            barScale = 0;

        healthBar.localScale = new Vector3(barScale, healthBar.localScale.y);
    }


    //equips weapon how u can see.
    //iff want some effects and gui updates when change wep. 
    //dat goes here.
    public void EquipWeapon(int _id)
    {
        weapon = database.FetchWeaponByID(_id);
        weaponeImage.sprite = weapon.sprite;
    }

    //and same shit for armor
    public void EquipArmor(int _id)
    {
        armor = database.FetchArmorByID(_id);

    }

}
