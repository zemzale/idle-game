using UnityEngine;

[System.Serializable]
public class Stage
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string[] enemys;

    private int currentIndx = 0;

    public string Name
    {
        get
        {
            return name;
        }
    }

    //Geting next enemy by index.
    //IF all done then send DONE
    //Check for it and ez lyfe.
    public string NextEnemy()
    {
        if (currentIndx <= enemys.Length - 1)
        {
            currentIndx++;
            return enemys[currentIndx - 1];
        }
        else
        {
            return "DONE";
        }
    }


    //TODO: GET THIS HOOKED TO PLAYER SMH.
    //RESTS INDEX SO EVERYTHING STARTS AGAIN.
    public void Reset()
    {
        currentIndx = 0;
    }
}
