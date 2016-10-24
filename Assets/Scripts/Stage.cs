using UnityEngine;

[System.Serializable]
public class Stage
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string[] enemies;

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
        Debug.LogWarning("Getting enemy with indx : " + currentIndx);
        if (currentIndx <= enemies.Length - 1)
        {
            currentIndx++;
            return enemies[currentIndx - 1];
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
