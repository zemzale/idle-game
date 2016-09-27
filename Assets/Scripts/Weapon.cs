using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {
    
    //TODO: Get some protection xd
    public int ID;
    public string name;
    public int damage;
    public int speed;
    public int accuracy;

    public Weapon (int _ID, string _name, int _damage, int _speed, int _accuracy)
    {
        ID = _ID;
        name = _name;
        damage = _damage;
        speed = _speed;
        accuracy = _accuracy;
    }
    
}
