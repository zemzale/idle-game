using UnityEngine;

[RequireComponent(typeof(LevelManager))]
public class GameManager : MonoBehaviour {

    public static GameManager singelton;

    private LevelManager levelManager;

    //ref to player and enemy. might need to change name of class player
    //cuz makes no sense. lul.
    private Character player;
    private Character enemy;

    void Start ()
    {
        singelton = this;
        //if null u done goof. check inspector.
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Character>();            
        }
        if (enemy == null)
        {
            enemy = GameObject.Find("Enemy").GetComponent<Character>();
        }

        levelManager = GetComponent<LevelManager>();
    }

    public Character GetPlayer ()
    {
        if (player != null)
            return player;
        else
        {
            Debug.LogError("Cant return palyer!");
            return null;
        }
    }

    //for timeing shit. hwatever.
    private float playerTimeToAttack;
    private float enemyTimeToAttcak;

    void Update ()
    {

        //do ze attaks. preaty simple.
        if (playerTimeToAttack < Time.time)
        {
            playerTimeToAttack = Time.time + 1 / (player.stats.AttackSpeed + player.weapon.speed) * player.stats.modAttackSpeed;
            player.Attack(enemy);
        }

        if (enemyTimeToAttcak < Time.time)
        {
            enemyTimeToAttcak = Time.time + 1 / (enemy.stats.AttackSpeed + enemy.weapon.speed) * enemy.stats.modAttackSpeed;
            enemy.Attack(player);
        }
    }



}
