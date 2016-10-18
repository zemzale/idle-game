using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindowManager : MonoBehaviour {

    private float offset;
    

    [SerializeField]
    private float lerpTime = 1;

    [SerializeField]
    private RectTransform gameWindow;
    [SerializeField]
    private RectTransform settingsWindow;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button backToGameWindowSettingsButton;

    void Start()
    {
        offset = gameWindow.rect.width;

        settingsButton.onClick.AddListener(() => {
            StartCoroutine(ChangeToWindows(gameWindow, settingsWindow, true));
        });

        backToGameWindowSettingsButton.onClick.AddListener(() =>
        {
            StartCoroutine(ChangeToWindows(settingsWindow, gameWindow, false));
        });
    }



    
    

    //Changes window with smooth effect. Nice meme.
    IEnumerator ChangeToWindows(RectTransform curWin, RectTransform tarWin, bool right)
    {

        float currentLerpTime = 0f;
        float dir;
        if (right)
            dir = offset;
        else
            dir = -offset;

        Vector3 start1 = curWin.localPosition;
        Vector3 start2 = tarWin.localPosition;

        Vector3 target1 = new Vector3(dir, 0f);
        Vector3 target2 = new Vector3(0f, 0f);
        

        while (tarWin.localPosition != target2)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float perc = currentLerpTime / lerpTime;
            curWin.localPosition = Vector3.Lerp(start1, target1, perc);
            tarWin.localPosition = Vector3.Lerp(start2, target2, perc);
            yield return null;
        }
        
    } 
}
