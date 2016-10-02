

[System.Serializable]
public class CharacterStats {

    //Defaults are hardcoded. Everything eles is done through modifiers.

    private string name;

    //Dafault values
    private int defHealth = 500;
    private float defDefense = 0;
    private float defDamage = 50;
    private float defAttackSpeed = 2;
    private float defAccuracy = 10;
    private float defDexterity = 10;

    //Value modifiers
    public float modHealth;
    public float modDefense;
    public float modDamage;
    public float modAttackSpeed;
    public float modAccuracy;
    public float modDexterity;

    public string Name
    {
        get { return name; }
    }
    //Getters
    public int Health
    {
        get { return (int)(defHealth * modHealth); } 
    }
    public float Defese
    {
        get { return modDefense;}
    }
    public float Damage
    {
        get{ return defDamage * modDamage;}
    }

    public float AttackSpeed
    {
        get { return defAttackSpeed * modAttackSpeed;}
    }
    public float Accuracy
    {
        get { return defAccuracy * modAccuracy;}
    }
    public float Dexterity
    {
        get { return defDexterity * modDexterity;}
    }

    //Fok dis for now
    public int Level;
    public int XP;

    public CharacterStats() {
        name = "NA";
        modHealth = 1;
        modDefense = 1;
        modDamage = 1;
        modAttackSpeed = 1;
        modAccuracy = 1;
        modDexterity = 1;
    }
    
    public CharacterStats(string _name, int _health , int _defense, int _damage,
               int _attackSpeed, int _accuracy, int _dexterity)
    {
        name = _name;
        modHealth = (float)_health / 10;
        modDefense = (float)_defense / 10;
        modDamage = (float)_damage / 10;
        modAttackSpeed = (float)_attackSpeed / 10;
        modAccuracy = (float)_accuracy / 10;
        modDexterity = (float)_dexterity / 10;
    }
}
