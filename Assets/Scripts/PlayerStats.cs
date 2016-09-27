

[System.Serializable]
public class PlayerStats {

    //Defaults are hardcoded. Everything eles is done through modifiers.

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

   
}
