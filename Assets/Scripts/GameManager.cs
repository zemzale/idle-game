using UnityEngine;

[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(SaveManager))]
public class GameManager : MonoBehaviour {

    public static GameManager singelton;

    public static bool pause = true;

    //ref to player and enemy. might need to change name of class player
    //cuz makes no sense. lul.
    private Character player;
    private Character enemy;

    private SaveManager saveManager;

    void Start ()
    {
        singelton = this;
        saveManager = GetComponent<SaveManager>();
        //if null u done goof. check inspector.
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Character>();            
        }
        if (enemy == null)
        {
            enemy = GameObject.Find("Enemy").GetComponent<Character>();
        }
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
        if (!pause)
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

#if UNITY_EDITOR
    void OnApplicationQuit()
    {
        saveManager.SaveGame();
    }
#elif UNITY_ANDROID
    //TODO: Test if this works how its suposed. maybe can change to OnAppDestroy() thoe wouldnt be so bad.
    void OnApplicationPause()
    {
        saveManager.SaveGame();
    }
#endif


}
