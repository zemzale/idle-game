using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    //ref to player and enemy. might need to change name of class player
    //cuz makes no sense. lul.
    public Player player;
    public Player enemy;

    

    void Start ()
    {
        //if null u done goof. check inspector.
        if (player == null)
        {
            Debug.LogError("No player found!");
        }
        if (enemy == null)
        {
            Debug.LogError("No enemy found!");
        }
    }

    //for timeing shit. hwatever.
    private float playerTimeToAttack;
    private float enemyTimeToAttcak;

    void Update ()
    {
        //do ze attaks. preaty simple.
        if (playerTimeToAttack < Time.time )
        {
            playerTimeToAttack = Time.time + 1 / player.attackSpeed;
            player.Attack(enemy);
        }

        if (enemyTimeToAttcak < Time.time)
        {
            enemyTimeToAttcak = Time.time + 1 / enemy.attackSpeed;
            enemy.Attack(player);
        }
    }
}
