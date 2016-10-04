using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager singelton;

    [SerializeField]
    private string[] trainingStage = new string[5];

    private int currentIndx = 0;

    void Start ()
    {
        singelton = this;
    }

    public string NextEnemy()
    {
        Debug.Log("Feeding Next enemy!");
        currentIndx++;
        return trainingStage[currentIndx - 1];
    }
}
