using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {
    
    //TODO: Get some protection xd
    public int ID;
    public string name;
    public int damage;

    public Weapon (int _ID, string _name, int _damage)
    {
        ID = _ID;
        name = _name;
        damage = _damage;
    }
    
}
