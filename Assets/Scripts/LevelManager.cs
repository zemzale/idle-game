﻿using UnityEngine;
using System.Collections.Generic;

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
        //TODO: CHECK FOR OUT OF INDEX ERROR.
        currentStage = stages[currentIndx];
        currentIndx++;
    }

    //Karoča tu paņem enemy no tagadeja stage.
    //Ja tu kruts un visus nositi dabū jauno stage. 
    //And ez lyfe.
    public string NextEnemy()
    {
        string tmp = currentStage.NextEnemy();
        Debug.LogWarning("Geting next enemy!" + tmp);
        if (tmp.Equals("DONE") == true)
        {
            NextStage();
            NextEnemy();
            return null;
        }
        else
            return tmp;
    }

    
}
