using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager singelton;

    [SerializeField]
    private Stage[] stages;
    private Stage currentStage; 

    private int currentIndx = 0;

    void Start ()
    {
        singelton = this;
        NextStage();
    }

    private void NextStage()
    {
        if (currentIndx == stages.Length)
        {
            Debug.LogWarning("There is no more stages. Git fu");
            return;
        }
        currentStage = stages[currentIndx];
        currentIndx++;
    }

    //Karoča tu paņem enemy no tagadeja stage.
    //Ja tu kruts un visus nositi dabū jauno stage. 
    //And ez lyfe.
    public string GetNextEnemy()
    {
        string tmp = currentStage.NextEnemy();          //Saņem tmp var ar enemie nosaukumu.
        Debug.LogWarning("Geting next enemy!" + tmp);
        if (tmp != null)
        {
            if (tmp.Equals("DONE") == true)                 
            {
                NextStage();
                string next = currentStage.NextEnemy();
                return next; 
            }
            else
                return tmp;
        }
        else
        {
            Debug.LogError("No more enmies! git fucked!");
            return null;
        }
        
    }

    
}
