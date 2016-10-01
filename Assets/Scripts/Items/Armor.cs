using UnityEngine;

[System.Serializable]
public class Armor {

    public int ID;
    public string name;
    public int defense;
    public int bonusHP;
    public int dexterity;





    //Constructor
    public Armor (int _ID, string _name, int _defense, int _hp, int _dexterity)
    {
        ID = _ID;
        name = _name;
        defense = _defense;
        bonusHP = _hp;
        dexterity = _dexterity;
    }
}
