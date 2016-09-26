using UnityEngine;

public class Player : MonoBehaviour {


    #region Stats
    protected int maxHealth = 1000;
    [SerializeField]
    protected int currentHealth;
    
    //Attack modifer and speed.
    protected float attackMod = 1.2f;
    public float attackSpeed = 0.5f;

    //FOK DIS FOR NOW
    protected float deffence;
    protected float dexterity;
    protected float accuracy;

    protected int level;
    protected int xp;

    #endregion

    //If u dead then tru else fals. U know how dis goes.
    protected bool isDead = false;

    //ref to current wep. and database too. 
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private ItemDatabase database;
    //and healthbar ref.
    [SerializeField]
    protected RectTransform healthBar;


    void Start()
    {
        //u want wep. dis should changed to number that is linked to ur player acc or smth.
        EquipWeapon(1);
        //Dis too should change later.
        currentHealth = maxHealth;
        
        //if no healthbar u done goof. addd in inspector knub.
        if (healthBar == null)
        {
            Debug.LogError("No health bar!");
        }
    }

    //later for cases if u suck and die.
    protected void SetDefaults()
    {
        currentHealth = maxHealth;
        isDead = true;
    }

    //Called when u attack.
    public void Attack(Player obj)
    {
        obj.TakeDamage(DoDamage());
    }

    //Method called when its time to attack.
    //Callculates dmg and shit.
    public int DoDamage()
    {
        //TODO: Add calculation.
        float dmg = weapon.damage * attackMod;

        Debug.Log(transform.name + " attacked for " + dmg);
        return (int)dmg;
    }

    //Method called when takeing damage.
    public void TakeDamage(int amount)
    {
        if (!isDead)
        {
            Debug.Log(transform.name + " took " + amount + " damage.");
            currentHealth -= amount;
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
        float barScale = ((currentHealth * 100) / maxHealth) * 0.01f;
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

    }

}
