using UnityEngine;

[System.Serializable]
public class Weapon {
    
    //TODO: Get some protection xd
    public int ID;
    public string name;
    public int damage;
    public int speed;
    public int accuracy;
    public Sprite sprite;
    public string slug;

    public Weapon (int _ID, string _name, int _damage, int _speed, int _accuracy, string _slug)
    {
        ID = _ID;
        name = _name;
        damage = _damage;
        speed = _speed;
        accuracy = _accuracy;
        slug = _slug;
        sprite = Resources.Load<Sprite>("Sprites/Weapons/" + _slug);

    }
    
}
