using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager singelton;

    [SerializeField]
    private Text currentStageText;

    [SerializeField]
    private Stage[] stages;
    private Stage currentStage; 

    private int currentIndx = 0;

    public int CurrentIndx
    {
        get { return currentIndx; }
        set { currentIndx = value; }
    }

    void Start ()
    {
        singelton = this;
        NextStage();
    }

    private void NextStage()
    {
        if (currentIndx == stages.Length)
        {
            //TODO: Add some endgame shit!
            Debug.LogError("There is no more stages. Git fuked");
            return;
        }
        currentStage = stages[currentIndx];
        SetCurrentStageText();
        currentIndx++;
    }

    private void SetCurrentStageText ()
    {
        currentStageText.text = (currentIndx + 1).ToString();
    }

    //Karoča tu paņem enemy no tagadeja stage.
    //Ja tu kruts un visus nositi dabū jauno stage. 
    //And ez lyfe.
    public string GetNextEnemy()
    {
        string tmp = currentStage.NextEnemy();          //Saņem tmp var ar enemie nosaukumu.
        Debug.Log("Geting next enemy!" + tmp);
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

    public void ResetStage ()
    {
        currentStage.Reset();
        Debug.Log("Reseting Stage!");
    }
}
